using BrainMate.Data.Entities;
using BrainMate.Infrastructure.Context;
using BrainMate.Infrastructure.InfrastructureBases;
using BrainMate.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrainMate.Infrastructure.Repositories
{
	public class RelativesRepository : GenericRepositoryAsync<Relatives>, IRelativesRepository
	{
		#region Fields
		private readonly DbSet<Relatives> _relatives;

		#endregion
		#region Constructor
		public RelativesRepository(ApplicationDbContext context) : base(context)
		{
			_relatives = context.Set<Relatives>();
		}
		#endregion

		#region Handle Functions
		public async Task<List<Relatives>> GetRelativesAsync()
		{
			var relatives = await _dbContext.relatives.ToListAsync();
			return relatives;
		}
		#endregion
	}
}
