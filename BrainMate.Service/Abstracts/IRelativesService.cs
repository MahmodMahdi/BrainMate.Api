using BrainMate.Data.Entities;

namespace BrainMate.Service.Abstracts
{
	public interface IRelativesService
	{
		public Task<List<Relatives>> GetRelativesListAsync();
	}
}
