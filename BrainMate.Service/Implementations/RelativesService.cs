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
    public class RelativesService : IRelativesService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHost;
        private readonly UserManager<Patient> _userManager;
        #endregion

        #region Constructors
        public RelativesService(IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHost,
            IFileService fileService,
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
        public IQueryable<Relatives> FilterRelativesPaginatedQueryable(string search)
        {
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var user = _userManager.Users.FirstOrDefault(x => x.Email == patientEmailClaim);
            // Patient
            if (user!.PatientEmail == null)
            {
                var queryable = Helper(search);
                var result = queryable!.Where(x => x.PatientEmail == user!.PatientEmail);
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
        public async Task<Relatives> GetByIdAsync(int id)
        {
            var context = _httpContextAccessor.HttpContext!.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var relative = await _unitOfWork.relatives.GetByIdAsync(id);
            if (relative != null)
            {
                if (relative.Image != null)
                {
                    relative.Image = baseUrl + relative.Image;
                }
            }
            return relative!;
        }
        public async Task<Relatives> GetRelativeAsync(int id)
        {
            var relative = await _unitOfWork.relatives.GetByIdAsync(id);
            return relative;
        }

        public async Task<string> AddAsync(Relatives relative, IFormFile file)
        {
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var caregiver = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmailClaim);
            var patientEmail = caregiver?.PatientEmail;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmail);
            if (user?.Id == null)
                return "PatientDeleteHisEmail";
            var imageUrl = await _fileService.UploadImage("Relatives", file);
            relative.Image = imageUrl;
            switch (imageUrl)
            {
                case "NoImage": return "NoImage";
                case "FailedToUploadImage": return "FailedToUploadImage";
            }
            // Add
            try
            {
                relative.PatientEmail = user?.Email;
                relative.CaregiverEmail = caregiver!.Email;
                await _unitOfWork.relatives.AddAsync(relative);
                return "Success";
            }
            catch (Exception)
            {
                return "FailedToAdd";
            }
        }
        public async Task<string> UpdateAsync(Relatives relative, IFormFile file)
        {
            var OldUrl = relative.Image!;
            var UrlRoot = _webHost.WebRootPath;
            var path = $"{UrlRoot}{OldUrl}";

            var imageUrl = await _fileService.UploadImage("Relatives", file);
            if (OldUrl != null)
            {
                System.IO.File.Delete(path);
            }

            relative.Image = imageUrl;
            switch (imageUrl)
            {
                case "NoImage": return "NoImage";
                case "FailedToUploadImage": return "FailedToUpdateImage";
            }
            try
            {
                await _unitOfWork.relatives.UpdateAsync(relative);
                return "Success";
            }
            catch
            {
                return "FailedToUpdate";
            }
        }
        public async Task<string> DeleteAsync(Relatives relative)
        {
            var transaction = await _unitOfWork.relatives.BeginTransactionAsync();
            try
            {
                var OldUrl = relative.Image!;
                var UrlRoot = _webHost.WebRootPath;
                var path = $"{UrlRoot}{OldUrl}";
                await _unitOfWork.relatives.DeleteAsync(relative);
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

        public async Task<bool> IsPhoneExist(string phone)
        {
            var item = await _unitOfWork.relatives.GetTableNoTracking()
                                                  .Where(x => x.PhoneNumber!.Equals(phone))
                                                  .FirstOrDefaultAsync();
            var ExistPhone = await _unitOfWork.patients.GetTableNoTracking()
                .Where(x => x.PhoneNumber!.Equals(phone))
                                            .FirstOrDefaultAsync();

            if (item == null && ExistPhone == null) { return false; }
            else return true;
        }
        public async Task<bool> IsPhoneExcludeSelf(string phone, int id)
        {
            var item = await _unitOfWork.relatives.GetTableNoTracking()
                                                  .Where(x => x.PhoneNumber!.Equals(phone) && !x.Id.Equals(id))
                                                  .FirstOrDefaultAsync();
            var ExistPhone = await _unitOfWork.patients.GetTableNoTracking()
              .Where(x => x.PhoneNumber!.Equals(phone))
                                          .FirstOrDefaultAsync();
            if (item == null && ExistPhone == null) { return false; }
            else return true;
        }

        public IQueryable<Relatives> Helper(string search)
        {
            var queryable = _unitOfWork.relatives.GetTableNoTracking().OrderBy(x => x.RelationShipDegree).AsQueryable();
            if (search != null)
            {
                queryable = queryable.Where(x => x.Name!.Contains(search));
            }
            return queryable;
        }
        #endregion
    }
}
