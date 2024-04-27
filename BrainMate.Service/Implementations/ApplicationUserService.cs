using BrainMate.Data.Entities.Identity;
using BrainMate.Infrastructure.Context;
using BrainMate.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;

namespace BrainMate.Service.Implementations
{
    public class ApplicationUserService : IApplicationUserService
    {
        #region Fields
        private readonly UserManager<Patient> _patientManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _context;
        private readonly IUrlHelper _urlHelper;
        #endregion

        #region Constructor
        public ApplicationUserService(UserManager<Patient> patientManager,
                                       IHttpContextAccessor httpContextAccessor,
                                        IEmailService emailService,
                                       ApplicationDbContext context,
                                       IUrlHelper urlHelper)
        {
            _patientManager = patientManager;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _context = context;
            _urlHelper = urlHelper;
        }
        #endregion

        #region Handle Functions
        public async Task<string> RegisterAsync(Patient patient, string password)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // If Email is exist
                var OldPatient = await _patientManager.FindByEmailAsync(patient.Email!);

                // Email is Already Exist
                if (OldPatient != null) return "EmailIsExist";

                //Create
                var Result = await _patientManager.CreateAsync(patient, password!);

                // Failed
                if (!Result.Succeeded) return string.Join(",", Result.Errors.Select(x => x.Description).ToList());
                await _patientManager.AddToRoleAsync(patient, "patient");

                // Send Confirm Email
                var code = await _patientManager.GenerateEmailConfirmationTokenAsync(patient);
                var requestAccessor = _httpContextAccessor.HttpContext!.Request;
                var returnUrl = requestAccessor.Scheme + "://" + requestAccessor.Host + _urlHelper
                    .Action("ConfirmEmail", "Authentication", new { patientId = patient.Id, Code = code });
                var message = $"Please Confirm your account by <a href='{HtmlEncoder.Default.Encode(returnUrl)}'>Click here</a>.";
                await _emailService.SendEmailAsync(patient.Email!, message, "Confirmation Email");
                await transaction.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return "Failed";
            }
        }
        public async Task<string> CaregiverRegisterAsync(Patient caregiver, string password)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // If Email is exist
                var OldUser = await _patientManager.FindByEmailAsync(caregiver.Email!);
                // Caregiver Email is Already Exist 
                if (OldUser != null) return "EmailIsExist";
                // check if another caregiver exist with this patient email
                var OldPatient = await _patientManager.Users.FirstOrDefaultAsync(x => x.PatientEmail == caregiver.PatientEmail!);
                if (OldPatient != null) return "ThereIsAnotherCaregiverOnThisPatientEmail";
                // check if there any email in db equal the entered patient email (access بيتأكد اذا فعلا فيه ايميل لمريض متسجل قبل كده وبيساوي الايميل اللى المرافق عايز ي )
                var ExistPatient = await _patientManager.Users.AnyAsync(x => x.Email == caregiver.PatientEmail);
                //Create
                if (ExistPatient == true)
                {
                    // create email to db
                    var Result = await _patientManager.CreateAsync(caregiver, password!);

                    // Failed
                    if (!Result.Succeeded) return string.Join(",", Result.Errors.Select(x => x.Description).ToList());
                    await _patientManager.AddToRoleAsync(caregiver, "Caregiver");
                    // Send Confirm Email
                    var code = await _patientManager.GenerateEmailConfirmationTokenAsync(caregiver);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var requestAccessor = _httpContextAccessor.HttpContext!.Request;
                    var returnUrl = requestAccessor.Scheme + "://" + requestAccessor.Host + _urlHelper
                        .Action("ConfirmEmail", "Authentication", new { patientId = caregiver.Id, Code = code });
                    // send message to email with encrypt code
                    var message = $"Please Confirm your account by <a href='{HtmlEncoder.Default.Encode(returnUrl)}'>Click here</a>.";
                    await _emailService.SendEmailAsync(caregiver.PatientEmail!, message, "Confirmation Email");
                    await transaction.CommitAsync();
                    return "Success";
                }
                else
                {
                    return "This Patient Email is not exist";
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return "Failed";
            }
        }
        #endregion
    }
}
