using BrainMate.Data.Entities;
using BrainMate.Infrastructure.Context;
using BrainMate.Infrastructure.InfrastructureBases;
using BrainMate.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrainMate.Infrastructure.Repositories
{
	public class PatientRepository : GenericRepositoryAsync<Patient>, IPatientRepository
	{
		#region fields
		private readonly DbSet<Patient> _patients;
		#endregion
		#region Constructors
		public PatientRepository(ApplicationDbContext _context) : base(_context)
		{
			_patients = _context.Set<Patient>();
		}
		#endregion
	}
}
