using BrainMate.Data.Entities;
using BrainMate.Infrastructure.InfrastructureBases;

namespace BrainMate.Infrastructure.Interfaces
{
	public interface IRelativesRepository : IGenericRepositoryAsync<Relatives>
	{
		public Task<List<Relatives>> GetInstructorsAsync();
	}
}
