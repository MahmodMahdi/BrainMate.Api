using BrainMate.Data.Entities;
using BrainMate.Infrastructure.Interfaces;
using BrainMate.Service.Abstracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SchoolProject.Service.Abstracts;

namespace BrainMate.Service.Implementations
{
	public class PatientService : IPatientService
	{
		#region Fields
		private readonly IPatientRepository _patientRepository;
		private readonly IFileService _fileService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IWebHostEnvironment _webHost;
		#endregion
		#region Constructor
		public PatientService(IPatientRepository patientRepository,
			IFileService fileService,
			IHttpContextAccessor httpContextAccessor,
			IWebHostEnvironment webHost)
		{
			_patientRepository = patientRepository;
			_fileService = fileService;
			_httpContextAccessor = httpContextAccessor;
			_webHost = webHost;
		}
		#endregion
		#region Handle Functions

		public async Task<Patient> GetPatientByIdAsync(int id)
		{
			var context = _httpContextAccessor.HttpContext!.Request;
			var baseUrl = context.Scheme + "://" + context.Host;
			var patient = await _patientRepository.GetByIdAsync(id);
			if (patient != null)
			{
				if (patient.Image != null)
				{
					patient.Image = baseUrl + patient.Image;
				}
			}
			return patient!;
		}
		public async Task<Patient> GetPatientAsync(int id)
		{
			var patient = await _patientRepository.GetByIdAsync(id);
			return patient;
		}

		public async Task<string> AddAsync(Patient patient, IFormFile file)
		{
			var imageUrl = await _fileService.UploadImage("Patient", file);
			patient.Image = imageUrl;
			switch (imageUrl)
			{
				case "NoImage": return "NoImage";
				case "FailedToUploadImage": return "FailedToUploadImage";
			}
			var ExistPatient = _patientRepository.
				GetTableNoTracking()
				.Where(x => x.NameEn!.Equals(patient.NameEn))
				.FirstOrDefault();
			if (ExistPatient != null) return "Exist";
			// Add
			try
			{
				await _patientRepository.AddAsync(patient);
				return "Success";
			}
			catch (Exception)
			{
				return "FailedToAdd";
			}
		}

		public async Task<string> UpdateAsync(Patient patient, IFormFile file)
		{
			var OldUrl = patient.Image!;
			var UrlRoot = _webHost.WebRootPath;
			var path = $"{UrlRoot}{OldUrl}";
			var imageUrl = await _fileService.UploadImage("Patient", file);
			if (OldUrl != null)
			{
				System.IO.File.Delete(path);
			}
			patient.Image = imageUrl;
			switch (imageUrl)
			{
				case "NoImage": return "NoImage";
				case "FailedToUploadImage": return "FailedToUpdateImage";
			}
			try
			{
				await _patientRepository.UpdateAsync(patient);
				return "Success";
			}
			catch
			{
				return "FailedToUpdate";
			}
		}
		#endregion

	}
}
