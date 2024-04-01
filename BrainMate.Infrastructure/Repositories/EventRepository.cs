using BrainMate.Data.Entities;
using BrainMate.Infrastructure.Context;
using BrainMate.Infrastructure.InfrastructureBases;
using BrainMate.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrainMate.Infrastructure.Repositories
{
	public class EventRepository : GenericRepositoryAsync<Event>, IEventRepository
	{
		#region Fields
		private readonly DbSet<Event> _event;

		#endregion
		#region Constructor
		public EventRepository(ApplicationDbContext context) : base(context)
		{
			_event = context.Set<Event>();
		}
		#endregion
		#region Handle Functions
		public async Task<List<Event>> GetEventsAsync()
		{
			var events = await _dbContext.events.ToListAsync();
			return events;
		}
		#endregion
	}
}
