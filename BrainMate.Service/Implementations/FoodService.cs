using BrainMate.Data.Entities;
using BrainMate.Infrastructure.Interfaces;
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
		private readonly IFoodRepository _foodRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IFileService _fileService;
		private readonly IWebHostEnvironment _webHost;
		#endregion
		#region Constructors
		public FoodService(IFoodRepository foodRepository,
			IHttpContextAccessor httpContextAccessor,
			IWebHostEnvironment webHost,
			IFileService fileService)
		{
			_foodRepository = foodRepository;
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
			var Foods = await _foodRepository.GetTableNoTracking().OrderBy(x => x.NameEn).ToListAsync();
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
			var Food = await _foodRepository.GetByIdAsync(id);
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
			var Food = await _foodRepository.GetByIdAsync(id);
			return Food;
		}
		public async Task<List<Food>> SearchAsync(string search)
		{
			var SearchString = await _foodRepository.GetTableNoTracking().Where(x => x.NameEn == search || x.NameAr == search).ToListAsync();
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
			var ExistPatient = _foodRepository.
				GetTableNoTracking()
				.Where(x => x.NameEn!.Equals(Food.NameEn))
				.FirstOrDefault();
			if (ExistPatient != null) return "Exist";
			// Add
			try
			{
				await _foodRepository.AddAsync(Food);
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
				await _foodRepository.UpdateAsync(Food);
				return "Success";
			}
			catch
			{
				return "FailedToUpdate";
			}
		}
		public async Task<string> DeleteAsync(Food Food)
		{
			var transaction = await _foodRepository.BeginTransactionAsync();
			try
			{
				var OldUrl = Food.Image!;
				var UrlRoot = _webHost.WebRootPath;
				var path = $"{UrlRoot}{OldUrl}";
				await _foodRepository.DeleteAsync(Food);
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
		#endregion
	}
}
