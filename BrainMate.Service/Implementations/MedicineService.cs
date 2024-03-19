﻿using BrainMate.Data.Entities;
using BrainMate.Infrastructure.Interfaces;
using BrainMate.Service.Abstracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Service.Abstracts;

namespace BrainMate.Service.Implementations
{
	public class MedicineService : IMedicineService
	{
		#region Fields
		private readonly IMedicineRepository _medicineRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IFileService _fileService;
		private readonly IWebHostEnvironment _webHost;
		#endregion
		#region Constructors
		public MedicineService(IMedicineRepository medicineRepository,
			IHttpContextAccessor httpContextAccessor,
			IWebHostEnvironment webHost,
			IFileService fileService)
		{
			_medicineRepository = medicineRepository;
			_httpContextAccessor = httpContextAccessor;
			_webHost = webHost;
			_fileService = fileService;
		}
		#endregion
		#region Handle Functions
		public IQueryable<Medicine> FilterMedicinesPaginatedQueryable()
		{
			var queryable = _medicineRepository.GetTableNoTracking().OrderBy(x => x.NameEn).AsQueryable();
			return queryable;
		}
		public async Task<List<Medicine>> SearchAsync(string search)
		{
			var SearchString = await _medicineRepository.GetTableNoTracking().Where(x => x.NameEn == search || x.NameAr == search).ToListAsync();
			return SearchString;
		}
		public async Task<Medicine> GetByIdAsync(int id)
		{
			var context = _httpContextAccessor.HttpContext!.Request;
			var baseUrl = context.Scheme + "://" + context.Host;
			var Medicine = await _medicineRepository.GetByIdAsync(id);
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
			var Medicine = await _medicineRepository.GetByIdAsync(id);
			return Medicine;
		}

		public async Task<string> AddAsync(Medicine Medicine, IFormFile file)
		{
			var imageUrl = await _fileService.UploadImage("Medicines", file);
			Medicine.Image = imageUrl;
			switch (imageUrl)
			{
				case "NoImage": return "NoImage";
				case "FailedToUploadImage": return "FailedToUploadImage";
			}
			var ExistPatient = _medicineRepository.
				GetTableNoTracking()
				.Where(x => x.NameEn!.Equals(Medicine.NameEn))
				.FirstOrDefault();
			if (ExistPatient != null) return "Exist";
			// Add
			try
			{
				await _medicineRepository.AddAsync(Medicine);
				return "Success";
			}
			catch (Exception)
			{
				return "FailedToAdd";
			}
		}
		public async Task<string> UpdateAsync(Medicine Medicine, IFormFile file)
		{
			var OldUrl = Medicine.Image!;
			var UrlRoot = _webHost.WebRootPath;
			var path = $"{UrlRoot}{OldUrl}";
			var imageUrl = await _fileService.UploadImage("Medicines", file);
			if (OldUrl != null)
			{
				System.IO.File.Delete(path);
			}

			Medicine.Image = imageUrl;
			switch (imageUrl)
			{
				case "NoImage": return "NoImage";
				case "FailedToUploadImage": return "FailedToUpdateImage";
			}
			try
			{
				await _medicineRepository.UpdateAsync(Medicine);
				return "Success";
			}
			catch
			{
				return "FailedToUpdate";
			}
		}
		public async Task<string> DeleteAsync(Medicine Medicine)
		{
			var transaction = await _medicineRepository.BeginTransactionAsync();
			try
			{
				var OldUrl = Medicine.Image!;
				var UrlRoot = _webHost.WebRootPath;
				var path = $"{UrlRoot}{OldUrl}";
				await _medicineRepository.DeleteAsync(Medicine);
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
