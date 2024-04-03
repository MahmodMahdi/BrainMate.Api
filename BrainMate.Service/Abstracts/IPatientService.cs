using BrainMate.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace BrainMate.Service.Abstracts
{
	public interface IPatientService
	{
		public Task<Patient> GetByIdAsync(int id);
		public Task<Patient> GetPatientAsync(int id);
		public Task<string> AddAsync(Patient patient, IFormFile file);
		public Task<string> UpdateAsync(Patient patient, IFormFile file);
	}
}
