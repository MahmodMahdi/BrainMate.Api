using BrainMate.Data.Entities;
using BrainMate.Infrastructure.InfrastructureBases;

namespace BrainMate.Infrastructure.Interfaces
{
	public interface IPatientRepository : IGenericRepositoryAsync<Patient>
	{
	}
}
