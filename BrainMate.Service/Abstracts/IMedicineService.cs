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

        public Task<bool> IsNameExist(string name);
        public Task<bool> IsNameExcludeSelf(string name, int id);
        public IQueryable<Medicine> FilterMedicinesPaginatedQueryable(string search);
    }
}
