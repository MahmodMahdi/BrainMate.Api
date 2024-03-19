using BrainMate.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace BrainMate.Service.Abstracts
{
	public interface IFoodService
	{
		public Task<List<Food>> GetAllAsync();
		public Task<Food> GetByIdAsync(int id);
		public Task<Food> GetFoodAsync(int id);
		public Task<string> AddAsync(Food food, IFormFile file);
		public Task<string> UpdateAsync(Food food, IFormFile file);
		public Task<string> DeleteAsync(Food food);
		public Task<List<Food>> SearchAsync(string search);
	}
}
