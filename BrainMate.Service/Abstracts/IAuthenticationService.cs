using BrainMate.Data.Entities.Identity;
using BrainMate.Data.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace BrainMate.Service.Abstracts
{
	public interface IAuthenticationService
	{
		#region User (Patient)
		public Task<JwtAuthenticationResponse> GetJWTToken(User user);
		public JwtSecurityToken ReadJwtToken(string Token);
		public Task<JwtAuthenticationResponse> GetRefreshToken(User user, JwtSecurityToken JwtToken, DateTime? ExpireDate, string RefreshToken);
		public Task<string> ValidateToken(string Token);
		public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string Token, string refreshToken);
		#endregion
		#region Email Confirmation
		public Task<string> ConfirmEmail(int? UserId, string Code);
		#endregion
		#region Reset Password
		public Task<string> SendResetPasswordCode(string Email);
		public Task<string> ConfirmResetPassword(string Code, string Email);
		public Task<string> ResetPassword(string Email, string Password);
		#endregion
	}
}
