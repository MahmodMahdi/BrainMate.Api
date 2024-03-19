using BrainMate.Data.Entities;
using BrainMate.Infrastructure.Interfaces;
using BrainMate.Service.Abstracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SchoolProject.Service.Abstracts;

namespace BrainMate.Service.Implementations
{
	public class RelativesService : IRelativesService
	{
		#region Fields
		private readonly IRelativesRepository _relativesRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IFileService _fileService;
		private readonly IWebHostEnvironment _webHost;
		#endregion
		#region Constructors
		public RelativesService(IRelativesRepository relativesRepository,
			IHttpContextAccessor httpContextAccessor,
			IWebHostEnvironment webHost,
			IFileService fileService)
		{
			_relativesRepository = relativesRepository;
			_httpContextAccessor = httpContextAccessor;
			_webHost = webHost;
			_fileService = fileService;
		}
		#endregion
		#region Handle Functions
		public IQueryable<Relatives> FilterRelativesPaginatedQueryable()
		{
			var queryable = _relativesRepository.GetTableNoTracking().OrderBy(x => x.RelationShipDegree).AsQueryable();
			return queryable;
		}
		public IQueryable<Relatives> FilterRelativesSearchQueryable(string search)
		{
			var SearchString = _relativesRepository.GetTableNoTracking().Where(x => x.NameEn == search || x.NameAr == search).AsQueryable();
			return SearchString;
		}
		public async Task<Relatives> GetByIdAsync(int id)
		{
			var context = _httpContextAccessor.HttpContext!.Request;
			var baseUrl = context.Scheme + "://" + context.Host;
			var relative = await _relativesRepository.GetByIdAsync(id);
			if (relative.Image != null)
			{
				relative.Image = baseUrl + relative.Image;
			}
			return relative;
		}
		public async Task<Relatives> GetRelativeAsync(int id)
		{
			var relative = await _relativesRepository.GetByIdAsync(id);
			return relative;
		}

		public async Task<string> AddAsync(Relatives relative, IFormFile file)
		{
			var imageUrl = await _fileService.UploadImage("Relatives", file);
			relative.Image = imageUrl;
			switch (imageUrl)
			{
				case "NoImage": return "NoImage";
				case "FailedToUploadImage": return "FailedToUploadImage";
			}
			var ExistPatient = _relativesRepository.
				GetTableNoTracking()
				.Where(x => x.NameEn!.Equals(relative.NameEn))
				.FirstOrDefault();
			if (ExistPatient != null) return "Exist";
			// Add
			try
			{
				await _relativesRepository.AddAsync(relative);
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
				await _relativesRepository.UpdateAsync(relative);
				return "Success";
			}
			catch
			{
				return "FailedToUpdate";
			}
		}
		public async Task<string> DeleteAsync(Relatives relative)
		{
			var transaction = await _relativesRepository.BeginTransactionAsync();
			try
			{
				var OldUrl = relative.Image!;
				var UrlRoot = _webHost.WebRootPath;
				var path = $"{UrlRoot}{OldUrl}";
				await _relativesRepository.DeleteAsync(relative);
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
