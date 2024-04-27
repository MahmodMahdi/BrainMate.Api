using BrainMate.Data.Entities;
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
    public class MedicineService : IMedicineService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHost;
        private readonly UserManager<Patient> _userManager;
        #endregion

        #region Constructors
        public MedicineService(IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHost,
            IFileService fileService,
            IUnitOfWork unitOfWork,
            UserManager<Patient> userManager)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _webHost = webHost;
            _fileService = fileService;
            _userManager = userManager;
        }
        #endregion

        #region Handle Functions
        public IQueryable<Medicine> FilterMedicinesPaginatedQueryable(string search)
        {
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var user = _userManager.Users.FirstOrDefault(x => x.Email == patientEmailClaim);
            // Patient
            if (user!.PatientEmail == null)
            {
                var queryable = Helper(search);
                var result = queryable!.Where(x => x.PatientEmail == user!.Email);
                if (result.Any(x => x.PatientEmail == user!.Email)) { return result.AsQueryable(); }
                else return result.AsQueryable();
            }
            // Caregiver
            else
            {
                var queryable = Helper(search);
                var result = queryable!.Where(x => x.CaregiverEmail == user!.Email);
                if (result.Any(x => x.CaregiverEmail == user!.Email)) { return result.AsQueryable(); }
                else return result.AsQueryable();
            }
        }
        public async Task<Medicine> GetByIdAsync(int id)
        {
            var context = _httpContextAccessor.HttpContext!.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var Medicine = await _unitOfWork.medicines.GetByIdAsync(id);
            if (Medicine != null)
            {
                if (Medicine.Image != null)
                {
                    Medicine.Image = baseUrl + Medicine.Image;
                }
            }
            return Medicine!;
        }
        public async Task<Medicine> GetMedicineAsync(int id)
        {
            var Medicine = await _unitOfWork.medicines.GetByIdAsync(id);
            return Medicine;
        }

        public async Task<string> AddAsync(Medicine Medicine, IFormFile file)
        {
            var patientIdClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var caregiver = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientIdClaim);
            var patientEmail = caregiver?.PatientEmail;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmail);
            if (user?.Id == null) return "PatientDeleteHisEmail";
            var imageUrl = await _fileService.UploadImage("Medicines", file);
            Medicine.Image = imageUrl;
            switch (imageUrl)
            {
                case "NoImage": return "NoImage";
                case "FailedToUploadImage": return "FailedToUploadImage";
            }
            // Add
            try
            {
                Medicine.PatientEmail = user?.Email;
                Medicine.CaregiverEmail = caregiver?.Email;
                await _unitOfWork.medicines.AddAsync(Medicine);
                return "Success";
            }
            catch (Exception)
            { return "FailedToAdd"; }
        }
        public async Task<string> UpdateAsync(Medicine Medicine, IFormFile file)
        {
            var OldUrl = Medicine.Image!;
            var UrlRoot = _webHost.WebRootPath;
            var path = $"{UrlRoot}{OldUrl}";
            var imageUrl = await _fileService.UploadImage("Medicines", file);
            if (OldUrl != null) { System.IO.File.Delete(path); }
            Medicine.Image = imageUrl;
            switch (imageUrl)
            {
                case "NoImage": return "NoImage";
                case "FailedToUploadImage": return "FailedToUpdateImage";
            }
            try
            {
                await _unitOfWork.medicines.UpdateAsync(Medicine);
                return "Success";
            }
            catch { return "FailedToUpdate"; }
        }
        public async Task<string> DeleteAsync(Medicine Medicine)
        {
            var transaction = await _unitOfWork.medicines.BeginTransactionAsync();
            try
            {
                var OldUrl = Medicine.Image!;
                var UrlRoot = _webHost.WebRootPath;
                var path = $"{UrlRoot}{OldUrl}";
                await _unitOfWork.medicines.DeleteAsync(Medicine);
                System.IO.File.Delete(path);
                await transaction.CommitAsync();
                return "Success";
            }
            catch
            {
                await transaction.RollbackAsync();
                return "Failed";
            }
        }

        public async Task<bool> IsNameExist(string name)
        {
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var caregiver = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmailClaim);
            var patientEmail = caregiver?.PatientEmail;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmail);
            if (user != null)
            {
                // check if the name exist or not
                var ExistMedicine = await _unitOfWork.medicines.GetTableNoTracking().Where(x => x.Name!.Equals(name) && x.PatientEmail == user!.Email).FirstOrDefaultAsync();
                if (ExistMedicine == null) { return false; }
                else return true;
            }
            else
            {
                var ExistMedicine = await _unitOfWork.medicines.GetTableNoTracking().Where(x => x.Name!.Equals(name) && x.CaregiverEmail == caregiver!.Email).FirstOrDefaultAsync();
                if (ExistMedicine == null) { return false; }
                else return true;
            }
        }
        public async Task<bool> IsNameExcludeSelf(string name, int id)
        {
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var caregiver = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmailClaim);
            var patientEmail = caregiver?.PatientEmail;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmail);
            // check if Name exclude self or exist in another field
            var ExistMedicine = await _unitOfWork.medicines.GetTableNoTracking().Where(x => x.Name!.Equals(name) && x.PatientEmail == user!.Email && x.Id != id).FirstOrDefaultAsync();
            if (ExistMedicine == null) { return false; }
            else return true;
        }

        public IQueryable<Medicine> Helper(string search)
        {
            var queryable = _unitOfWork.medicines.GetTableNoTracking().OrderBy(x => x.Name).AsQueryable();
            if (search != null)
            {
                queryable = queryable.Where(x => x.Name!.Contains(search));
            }
            return queryable;
        }


        #endregion
    }
}
