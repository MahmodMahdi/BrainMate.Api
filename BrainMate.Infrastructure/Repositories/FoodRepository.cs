using BrainMate.Data.Entities;
using BrainMate.Infrastructure.Context;
using BrainMate.Infrastructure.InfrastructureBases;
using BrainMate.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrainMate.Infrastructure.Repositories
{
	public class FoodRepository : GenericRepositoryAsync<Food>, IFoodRepository
	{
		#region Fields
		private readonly DbSet<Food> _food;

		#endregion
		#region Constructor
		public FoodRepository(ApplicationDbContext context) : base(context)
		{
			_food = context.Set<Food>();
		}
		#endregion
		#region Handle Functions
		public async Task<List<Food>> GetFoodAsync()
		{
			var food = await _dbContext.foods.ToListAsync();
			return food;
		}
		#endregion
	}
}
