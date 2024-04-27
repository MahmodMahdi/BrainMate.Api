using BrainMate.Data.Entities.Identity;
using BrainMate.Data.Helpers;
using BrainMate.Data.Responses;
using BrainMate.Infrastructure.Context;
using BrainMate.Infrastructure.Interfaces;
using BrainMate.Service.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BrainMate.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly jwtSettings _jwtSettings;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<Patient> _patientManager;
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _context;
        //private readonly IEncryptionProvider _encryptionProvider;
        #endregion

        #region Constructor
        public AuthenticationService(jwtSettings jwtSettings,
        IRefreshTokenRepository refreshTokenRepository,
            UserManager<Patient> patientManager,
            IEmailService emailService,
            ApplicationDbContext context)
        {
            _jwtSettings = jwtSettings;
            _refreshTokenRepository = refreshTokenRepository;
            _patientManager = patientManager;
            _emailService = emailService;
            _context = context;
        }
        #endregion

        #region Handle Functions

        ////////////////////////////////////
        #region patient (Patient)
        public async Task<JwtAuthenticationResponse> GetJWTToken(Patient patient)
        {
            #region Token
            var (jwtToken, Token) = await GenerateJwtToken(patient);
            #endregion
            #region RefreshToken
            var refreshToken = GetRefreshToken(patient.Email!);

            // save items of refresh token
            var patientRefreshToken = new PatientRefreshToken
            {
                PatientId = patient.Id,
                Token = Token,
                RefreshToken = refreshToken.refreshTokenString,
                JwtId = jwtToken.Id,
                IsUsed = true,
                IsRevoked = false,
                AddedTime = DateTime.Now,
                ExpireDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
            };
            await _refreshTokenRepository.AddAsync(patientRefreshToken);

            var result = new JwtAuthenticationResponse();
            result.RefreshToken = refreshToken;
            result.Token = Token;
            return result;
            #endregion
        }

        private async Task<(JwtSecurityToken, string)> GenerateJwtToken(Patient patient)
        {
            var claims = await GetClaims(patient);

            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(_jwtSettings.TokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret!)),
                SecurityAlgorithms.HmacSha256Signature));
            var Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, Token);
        }

        public async Task<List<Claim>> GetClaims(Patient patient)
        {
            var roles = await _patientManager.GetRolesAsync(patient);
            var claims = new List<Claim>()
             {
                new Claim(nameof(PatientClaimsModel.Id), patient.Id!.ToString()),
                new Claim(ClaimTypes.NameIdentifier,patient.UserName!),
                new Claim(ClaimTypes.Email, patient.Email!),
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var patientClaims = await _patientManager.GetClaimsAsync(patient);
            claims.AddRange(patientClaims);
            return claims;
        }

        private RefreshToken GetRefreshToken(string Email)
        {
            // Refresh Token
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                Email = Email,
                refreshTokenString = GenerateRefreshToken()
            };
            return refreshToken;
        }

        public async Task<JwtAuthenticationResponse> GetRefreshToken(Patient patient, JwtSecurityToken JwtToken, DateTime? ExpireDate, string RefreshToken)
        {
            var (jwtSecurityToken, newToken) = await GenerateJwtToken(patient!);

            var Result = new JwtAuthenticationResponse();
            Result.Token = newToken;
            var RefreshTokenResult = new RefreshToken();
            RefreshTokenResult.Email = JwtToken.Claims.FirstOrDefault(x => x.Type == nameof(PatientClaimsModel.Email))!.Value;
            RefreshTokenResult.refreshTokenString = RefreshToken;
            RefreshTokenResult.ExpireAt = (DateTime)ExpireDate!;
            Result.RefreshToken = RefreshTokenResult;
            return Result;
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string Token, string refreshToken)
        {
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature)) { return ("AlgorithmIsWrong", null); }
            if (jwtToken.ValidTo > DateTime.UtcNow) { return ("TokenIsNotExpired", null); }
            // Get patient
            var Id = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(PatientClaimsModel.Id))!.Value;
            var patientRT = await _refreshTokenRepository.GetTableNoTracking()
                .FirstOrDefaultAsync(x => x.Token == Token && x.RefreshToken == refreshToken && x.PatientId == int.Parse(Id));
            if (patientRT == null) return ("RefreshTokenIsNotFound", null);
            // Validations Token , RefreshToken
            if (patientRT!.ExpireDate < DateTime.UtcNow)
            {
                patientRT.IsRevoked = true;
                patientRT.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(patientRT);
                return ("RefreshTokenIsExpired", null);
            }

            var expireDate = patientRT.ExpireDate;
            return (Id, expireDate);
        }

        public JwtSecurityToken ReadJwtToken(string Token)
        {
            if (string.IsNullOrEmpty(Token)) throw new ArgumentNullException(nameof(Token));
            var handler = new JwtSecurityTokenHandler();
            var result = handler.ReadJwtToken(Token);
            return result;
        }

        public async Task<string> ValidateToken(string Token)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret!)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };
            try
            {
                // Validation
                var validator = handler.ValidateToken(Token, parameters, out SecurityToken validatedToken);
                if (validator == null) { throw new SecurityTokenException("Invalid Token"); }
                return "NotExpired";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion
        /////////////////////////////////////

        #region Confirm Email
        public async Task<string> ConfirmEmail(int? patientId, string Code)
        {
            if (patientId == null || Code == null) { return "ErrorInConfirmEmail"; }
            var patient = await _patientManager.FindByIdAsync(patientId.ToString()!);
            var ConfirmEmail = await _patientManager.ConfirmEmailAsync(patient!, Code);
            if (!ConfirmEmail.Succeeded) { return "ErrorInConfirmEmail"; }
            return "Success";
        }
        #endregion

        /////////////////////////////////////
        #region Reset Password
        // Enter Email To Send The Code on it
        public async Task<string> SendResetPasswordCode(string Email)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // get patient 
                var patient = await _patientManager.FindByEmailAsync(Email);
                // if not exist => not found
                if (patient == null) { return "patientNotFound"; }
                // generate random number
                Random generator = new Random();
                string random = generator.Next(0, 1000000).ToString("D6");
                // update patient in db code
                patient.Code = random;
                var Updated = await _patientManager.UpdateAsync(patient);
                if (!Updated.Succeeded) { return "ErrorInUpdatepatient"; }
                // Send code to email 
                var message = "Code to Reset Password : " + patient.Code;
                await _emailService.SendEmailAsync(patient.Email!, message, "Reset Password");
                await transaction.CommitAsync();
                return "Success";
            }

            catch (Exception)
            {
                await transaction.RollbackAsync();
                return "Failed";
            }
        }
        // To Verify The Code That Sent To Email
        public async Task<string> ConfirmResetPassword(string Code, string Email)
        {
            // get code from db
            var patient = await _patientManager.FindByEmailAsync(Email);
            if (patient == null) return "patientNotFound";
            // decrypt code from db
            var patientCode = patient.Code;
            if (patientCode == Code) return "Success";
            return "Failed";


        }
        // To Reset Pass
        public async Task<string> ResetPassword(string Email, string Password)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // get code from db
                var patient = await _patientManager.FindByEmailAsync(Email);
                if (patient == null) return "patientNotFound";
                await _patientManager.RemovePasswordAsync(patient);
                await _patientManager.AddPasswordAsync(patient, Password);
                await transaction.CommitAsync();
                return "Success";
            }

            catch (Exception)
            {
                await transaction.RollbackAsync();
                return "Failed";
            }
        }
        public async Task<string> SendCaregiverResetPasswordCode(string Email)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // get patient 
                var patient = await _patientManager.FindByEmailAsync(Email);
                // if not exist => not found
                if (patient == null) { return "patientNotFound"; }
                // generate random number
                Random generator = new Random();
                string random = generator.Next(0, 1000000).ToString("D6");
                // update patient in db code
                patient.Code = random;
                var Updated = await _patientManager.UpdateAsync(patient);
                if (!Updated.Succeeded) { return "ErrorInUpdatepatient"; }
                // Send code to email 
                var message = "Code to Reset Password : " + patient.Code;
                await _emailService.SendEmailAsync(patient.PatientEmail!, message, "Reset Password");
                await transaction.CommitAsync();
                return "Success";
            }

            catch (Exception)
            {
                await transaction.RollbackAsync();
                return "Failed";
            }
        }
        #endregion
        #region Helper
        private string GenerateRefreshToken()
        {
            var RandomNumber = new byte[32];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(RandomNumber);
            return Convert.ToBase64String(RandomNumber);
        }
        #endregion

        #endregion
    }
}
