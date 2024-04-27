using BrainMate.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace BrainMate.Service.Abstracts
{
    public interface IFoodService
    {
        public Task<List<Food>> GetAllAsync(string search);
        public Task<Food> GetByIdAsync(int id);
        public Task<Food> GetFoodAsync(int id);
        public Task<string> AddAsync(Food food, IFormFile file);
        public Task<string> UpdateAsync(Food food, IFormFile file);
        public Task<string> DeleteAsync(Food food);

        public Task<bool> IsNameExist(string name);
        public Task<bool> IsNameExcludeSelf(string name, int id);
    }
}
