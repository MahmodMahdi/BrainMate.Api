using BrainMate.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace BrainMate.Service.Abstracts
{
    public interface IRelativesService
    {
        public Task<Relatives> GetByIdAsync(int id);
        public Task<Relatives> GetRelativeAsync(int id);
        public Task<string> AddAsync(Relatives relatives, IFormFile file);
        public Task<string> UpdateAsync(Relatives relatives, IFormFile file);
        public Task<string> DeleteAsync(Relatives relatives);

        public IQueryable<Relatives> FilterRelativesPaginatedQueryable(string search);
        public Task<bool> IsPhoneExist(string phone);
        public Task<bool> IsPhoneExcludeSelf(string phone, int id);
    }
}
