using BrainMate.Data.Entities;

namespace BrainMate.Service.Abstracts
{
	public interface IEventService
	{
		public Task<Event> GetEventByIdAsync(int id);
		public Task<string> AddAsync(Event Event);
		public Task<string> UpdateAsync(Event Event);
		public Task<string> DeleteAsync(Event Event);
		public IQueryable<Event> FilterEventsPaginatedQueryable();
		public IQueryable<Event> FilterEventsSearchQueryable(string search);
	}
}
