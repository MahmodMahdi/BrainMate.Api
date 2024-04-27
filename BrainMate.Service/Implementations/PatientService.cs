using BrainMate.Data.Entities.Identity;
using BrainMate.Infrastructure.UnitofWork;
using BrainMate.Service.Abstracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BrainMate.Service.Implementations
{
    public class PatientService : IPatientService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHost;
        private readonly UserManager<Patient> _userManager;
        #endregion

        #region Constructor
        public PatientService(
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHost,
            IUnitOfWork unitOfWork,
            UserManager<Patient> userManager)
        {

            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
            _webHost = webHost;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        #endregion

        #region Handle Functions

        public async Task<Patient> GetByIdAsync(int id)
        {
            var context = _httpContextAccessor.HttpContext!.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmailClaim!);
            if (user!.PatientEmail == null)
            {
                return await Helper(user);
            }
            else
            {
                var patientEmail = user.PatientEmail;
                var result = await _unitOfWork.patients.GetTableNoTracking().FirstOrDefaultAsync(x => x.Email == patientEmail);
                return await Helper(result!);
            }
        }
        public async Task<Patient> GetPatientAsync(int id)
        {
            var patient = await _unitOfWork.patients.GetByIdAsync(id);
            return patient;
        }

        public async Task<string> UpdateAsync(Patient patient, IFormFile file)
        {
            var SearchByPhone = await _unitOfWork.patients
                                                 .GetTableNoTracking()
                                                 .Where(x => x.PhoneNumber!.Equals(patient!.PhoneNumber) && !x.Id.Equals(patient.Id))
                                                 .FirstOrDefaultAsync();
            var SearchPhone = await _unitOfWork.relatives
                                               .GetTableNoTracking()
                                               .Where(x => x.PhoneNumber!.Equals(patient!.PhoneNumber))
                                               .FirstOrDefaultAsync();

            if (SearchByPhone != null || SearchPhone != null) return "PhoneExist";

            var OldUrl = patient.Image!;
            var UrlRoot = _webHost.WebRootPath;
            var path = $"{UrlRoot}{OldUrl}";

            var imageUrl = await _fileService.UploadImage("Patient", file);
            patient.Image = imageUrl;

            if (OldUrl != null)
            {
                System.IO.File.Delete(path);
            }

            switch (imageUrl)
            {
                case "NoImage": return "NoImage";
                case "FailedToUploadImage": return "FailedToUpdateImage";
            }
            try
            {
                await _unitOfWork.patients.UpdateAsync(patient);
                return "Success";
            }
            catch { return "FailedToUpdate"; };

        }
        public async Task<string> DeleteAsync(Patient patient)
        {
            var transaction = await _unitOfWork.patients.BeginTransactionAsync();
            try
            {
                // Root
                var UrlRoot = _webHost.WebRootPath;

                // Get User from login
                var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
                var Patient = await _userManager.FindByEmailAsync(patientEmailClaim!);

                // Get OldUrl for image and delete it
                var patientOldUrl = Patient!.Image!;
                if (patientOldUrl != null)
                {
                    var patientPath = $"{UrlRoot}{patientOldUrl}";
                    System.IO.File.Delete(patientPath);
                }
                await _unitOfWork.patients.DeleteAsync(Patient!);
                await transaction.CommitAsync();
                return "Success";
            }
            catch
            {
                await transaction.RollbackAsync();
                return "Failed";
            }
        }

        public async Task<string> LockUnlockAsync(int id)
        {
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var patient = await _userManager.FindByEmailAsync(patientEmailClaim!);
            var caregiver = await _userManager.Users.FirstOrDefaultAsync(x => x.PatientEmail == patient!.Email);
            if (caregiver == null) { return "NotFound"; }
            if (caregiver.LockoutEnd == null || caregiver.LockoutEnd < DateTime.Now)
            {
                caregiver.LockoutEnd = DateTime.Now.AddYears(1);
                await _unitOfWork.CompleteAsync();
                return "LockedDone";
            }
            else
            {
                caregiver.LockoutEnd = DateTime.Now;
                await _unitOfWork.CompleteAsync();
                return "UnLockDone";
            }
        }

        #region Helper
        public async Task<Patient> Helper(Patient user)
        {
            var context = _httpContextAccessor.HttpContext!.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var patient = await _unitOfWork.patients.GetByIdAsync(user!.Id);
            if (patient != null)
            {
                if (patient.Image != null)
                {
                    patient.Image = baseUrl + patient.Image;
                }
            }
            return patient!;
        }
        #endregion
        #endregion

    }
}
