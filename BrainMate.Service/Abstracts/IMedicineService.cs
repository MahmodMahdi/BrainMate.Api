using BrainMate.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace BrainMate.Service.Abstracts
{
	public interface IMedicineService
	{
		public Task<Medicine> GetByIdAsync(int id);
		public Task<Medicine> GetMedicineAsync(int id);
		public Task<string> AddAsync(Medicine Medicines, IFormFile file);
		public Task<string> UpdateAsync(Medicine Medicines, IFormFile file);
		public Task<string> DeleteAsync(Medicine Medicines);

		public IQueryable<Medicine> FilterMedicinesPaginatedQueryable();
		public Task<List<Medicine>> SearchAsync(string search);
	}
}
