using BrainMate.Data.Entities;
using BrainMate.Infrastructure.UnitofWork;
using BrainMate.Service.Abstracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Service.Abstracts;

namespace BrainMate.Service.Implementations
{
	public class FoodService : IFoodService
	{
		#region Fields
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IFileService _fileService;
		private readonly IWebHostEnvironment _webHost;
		#endregion
		#region Constructors
		public FoodService(IUnitOfWork unitOfWork,
			IHttpContextAccessor httpContextAccessor,
			IWebHostEnvironment webHost,
			IFileService fileService)
		{
			_unitOfWork = unitOfWork;
			_httpContextAccessor = httpContextAccessor;
			_webHost = webHost;
			_fileService = fileService;
		}
		#endregion
		#region Handle Functions
		public async Task<List<Food>> GetAllAsync()
		{
			var context = _httpContextAccessor.HttpContext!.Request;
			var baseUrl = context.Scheme + "://" + context.Host;
			var Foods = await _unitOfWork.foods.GetTableNoTracking().OrderBy(x => x.Time).ToListAsync();
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
		public async Task<List<Food>> SearchAsync(string search)
		{
			var SearchString = await _unitOfWork.foods.GetTableNoTracking().Where(x => x.NameEn == search || x.NameAr == search).ToListAsync();
			return SearchString!;
		}
		public async Task<string> AddAsync(Food Food, IFormFile file)
		{
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
			// check if the name exist or not
			var food = await _unitOfWork.foods.GetTableNoTracking()
												  .Where(x => x.NameAr!.Equals(name) || x.NameEn!.Equals(name))
												  .FirstOrDefaultAsync();
			if (food == null) { return false; }
			else return true;
		}
		public async Task<bool> IsNameExcludeSelf(string name, int id)
		{
			// check if Name exclude self or exist in another field
			var food = await _unitOfWork.foods.GetTableNoTracking()
										.Where(x => x.NameEn!.Equals(name) && !x.Id.Equals(id)
											|| x.NameAr!.Equals(name) && !x.Id.Equals(id))
										.FirstOrDefaultAsync();
			if (food == null) { return false; }
			else return true;
		}
		#endregion
	}
}
