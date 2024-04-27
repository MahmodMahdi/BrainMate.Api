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
    public class CaregiverService : ICaregiverService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHost;
        private readonly UserManager<Patient> _userManager;
        #endregion

        #region Constructor
        public CaregiverService(
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
        public async Task<Patient> GetCaregiverByIdAsync(int id)
        {
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(patientEmailClaim!);
            if (user!.PatientEmail != null)
            {
                return await Helper(user);
            }
            else
            {
                var patientEmail = user.Email;
                var result = await _unitOfWork.patients.GetTableNoTracking().FirstOrDefaultAsync(x => x.PatientEmail == patientEmail);
                return await Helper(result!);
            }
        }
        public async Task<Patient> GetCaregiverAsync(int id)
        {
            var patient = await _unitOfWork.patients.GetByIdAsync(id);
            return patient;
        }

        public async Task<string> UpdateAsync(Patient caregiver, IFormFile file)
        {
            var SearchByPhone = await _unitOfWork.patients.GetTableNoTracking().Where(x => x.PhoneNumber!.Equals(caregiver!.PhoneNumber) && !x.Id.Equals(caregiver.Id))
                                              .FirstOrDefaultAsync();
            var SearchPhone = await _unitOfWork.relatives.GetTableNoTracking().Where(x => x.PhoneNumber!.Equals(caregiver!.PhoneNumber))
                                            .FirstOrDefaultAsync();
            if (SearchByPhone != null || SearchPhone != null) return "PhoneExist";

            var OldUrl = caregiver.Image!;
            var UrlRoot = _webHost.WebRootPath;
            var path = $"{UrlRoot}{OldUrl}";
            var imageUrl = await _fileService.UploadImage("Caregiver", file);
            caregiver.Image = imageUrl;
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
                await _unitOfWork.patients.UpdateAsync(caregiver);
                return "Success";
            }
            catch
            {
                return "FailedToUpdate";
            }
        }
        public async Task<string> DeleteAsync(Patient caregiver)
        {
            var transaction = await _unitOfWork.patients.BeginTransactionAsync();
            try
            {
                // Root
                var UrlRoot = _webHost.WebRootPath;

                #region Delete User 
                // Get User from login
                var caregiverEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
                var Caregiver = await _userManager.FindByEmailAsync(caregiverEmailClaim!);
                // Get OldUrl for image and delete it

                var caregiverOldUrl = Caregiver!.Image!;
                if (caregiverOldUrl != null)
                {
                    var patientPath = $"{UrlRoot}{caregiverOldUrl}";
                    System.IO.File.Delete(patientPath);
                }
                await _unitOfWork.patients.DeleteAsync(Caregiver!);
                #endregion

                await transaction.CommitAsync();

                return "Success";
            }
            catch
            {
                await transaction.RollbackAsync();
                return "Failed";
            }
        }

        public async Task<Patient> Helper(Patient patient)
        {
            var context = _httpContextAccessor.HttpContext!.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var caregiver = await _unitOfWork.patients.GetByIdAsync(patient!.Id);
            if (caregiver != null)
            {
                if (caregiver.Image != null)
                {
                    caregiver.Image = baseUrl + caregiver.Image;
                }
            }
            return caregiver!;
        }
        #endregion
    }
}
