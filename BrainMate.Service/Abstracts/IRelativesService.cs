using BrainMate.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace BrainMate.Service.Abstracts
{
	public interface IRelativesService
	{
		//	public Task<List<Relatives>> GetRelativesListAsync();
		public IQueryable<Relatives> FilterRelativesPaginatedQueryable();
		public Task<Relatives> GetByIdAsync(int id);
		public Task<Relatives> GetRelativeAsync(int id);
		public Task<string> AddAsync(Relatives relatives, IFormFile file);
		public Task<string> UpdateAsync(Relatives relatives, IFormFile file);
	}
}
