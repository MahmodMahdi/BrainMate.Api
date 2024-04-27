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
    public class FoodService : IFoodService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHost;
        private readonly UserManager<Patient> _userManager;
        #endregion

        #region Constructors
        public FoodService(IUnitOfWork unitOfWork,
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
        public async Task<List<Food>> GetAllAsync(string search)
        {
            // Get The User => From Login
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmailClaim);
            if (user!.PatientEmail == null)
            {
                // Patient
                // Helper => is a function that return a list of food
                var Foods = await Helper(search);
                var result = Foods!.Where(x => x.PatientEmail == user!.Email);
                if (result.Any(x => x.PatientEmail == user!.Email)) { return result.ToList(); }
                else return result.ToList();
            }
            else
            {
                // Caregiver
                // Helper => is a function that return a list of food
                var Foods = await Helper(search);
                var result = Foods!.Where(x => x.CaregiverEmail == user!.Email);
                if (result.Any(x => x.CaregiverEmail == user!.Email)) { return result.ToList(); }
                else return result.ToList();
            }
        }
        public async Task<Food> GetByIdAsync(int id)
        {
            var context = _httpContextAccessor.HttpContext!.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var Food = await _unitOfWork.foods.GetByIdAsync(id);
            if (Food != null)
            {
                if (Food.Image != null)
                {
                    Food.Image = baseUrl + Food.Image;
                }
            }
            return Food!;
        }
        public async Task<Food> GetFoodAsync(int id)
        {
            var Food = await _unitOfWork.foods.GetByIdAsync(id);
            return Food;
        }

        public async Task<string> AddAsync(Food Food, IFormFile file)
        {
            var userEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var caregiver = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == userEmailClaim);
            var patientEmail = caregiver?.PatientEmail;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmail);
            if (user?.Id == null)
                return "PatientDeleteHisEmail";
            var imageUrl = await _fileService.UploadImage("Foods", file);
            Food.Image = imageUrl;

            switch (imageUrl)
            {
                case "NoImage": return "NoImage";
                case "FailedToUploadImage": return "FailedToUploadImage";
            }
            // Add
            try
            {
                Food.PatientEmail = user?.Email;
                Food.CaregiverEmail = caregiver!.Email;
                await _unitOfWork.foods.AddAsync(Food);
                return "Success";
            }
            catch (Exception)
            {
                return "FailedToAdd";
            }
        }
        public async Task<string> UpdateAsync(Food Food, IFormFile file)
        {
            var OldUrl = Food.Image!;
            var UrlRoot = _webHost.WebRootPath;
            var path = $"{UrlRoot}{OldUrl}";
            var imageUrl = await _fileService.UploadImage("Foods", file);
            if (OldUrl != null)
            {
                System.IO.File.Delete(path);
            }

            Food.Image = imageUrl;
            switch (imageUrl)
            {
                case "NoImage": return "NoImage";
                case "FailedToUploadImage": return "FailedToUpdateImage";
            }
            try
            {
                await _unitOfWork.foods.UpdateAsync(Food);
                return "Success";
            }
            catch
            {
                return "FailedToUpdate";
            }
        }
        public async Task<string> DeleteAsync(Food Food)
        {
            var transaction = await _unitOfWork.foods.BeginTransactionAsync();
            try
            {
                // to delete item and remove the image 
                var OldUrl = Food.Image!;
                var UrlRoot = _webHost.WebRootPath;
                var path = $"{UrlRoot}{OldUrl}";
                await _unitOfWork.foods.DeleteAsync(Food);
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
                var ExistFood = await _unitOfWork.foods.GetTableNoTracking().Where(x => x.Name!.Equals(name) && x.PatientEmail == user!.Email).FirstOrDefaultAsync();
                if (ExistFood == null) { return false; }
                else return true;
            }
            else
            {
                var ExistFood = await _unitOfWork.foods.GetTableNoTracking().Where(x => x.Name!.Equals(name) && x.CaregiverEmail == caregiver!.Email).FirstOrDefaultAsync();
                if (ExistFood == null) { return false; }
                else return true;
            }
        }
        public async Task<bool> IsNameExcludeSelf(string name, int id)
        {
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var caregiver = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmailClaim);
            var patientEmail = caregiver?.PatientEmail;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmail);
            if (user != null)
            {
                // check if Name exclude self or exist in another field
                var ExistFood = await _unitOfWork.foods.GetTableNoTracking().Where(x => x.Name!.Equals(name) && x.PatientEmail == user!.Email && x.Id != id).FirstOrDefaultAsync();
                if (ExistFood == null) { return false; }
                else return true;
            }
            else
            {
                var ExistFood = await _unitOfWork.foods.GetTableNoTracking().Where(x => x.Name!.Equals(name) && x.CaregiverEmail == caregiver!.Email && x.Id != id).FirstOrDefaultAsync();
                if (ExistFood == null) { return false; }
                else return true;
            }
        }

        public async Task<List<Food>> Helper(string search)
        {
            var context = _httpContextAccessor.HttpContext!.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var Foods = await _unitOfWork.foods.GetTableNoTracking().OrderBy(x => x.Time).ToListAsync();
            if (search != null)
            {
                Foods = Foods.Where(x => x.Name!.Contains(search)).ToList();
            }
            if (Foods != null)
            {
                foreach (var food in Foods)
                {
                    if (food.Image != null)
                    {
                        food.Image = baseUrl + food.Image;
                    }
                }
            }
            return Foods!;
        }
        #endregion
    }
}
