using BrainMate.Data.Entities.Identity;
using BrainMate.Infrastructure.InfrastructureBases;

namespace BrainMate.Infrastructure.Interfaces
{
    public interface IPatientRepository : IGenericRepositoryAsync<Patient>
    {
    }
}
