using BrainMate.Data.Entities;
using BrainMate.Infrastructure.Context;
using BrainMate.Infrastructure.InfrastructureBases;
using BrainMate.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrainMate.Infrastructure.Repositories
{
	public class MedicineRepository : GenericRepositoryAsync<Medicine>, IMedicineRepository
	{
		#region Fields
		private readonly DbSet<Medicine> _medicine;

		#endregion
		#region Constructor
		public MedicineRepository(ApplicationDbContext context) : base(context)
		{
			_medicine = context.Set<Medicine>();
		}
		#endregion

		#region Handle Functions
		#endregion
	}
}
