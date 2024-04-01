using BrainMate.Data.Entities;
using BrainMate.Infrastructure.UnitofWork;
using BrainMate.Service.Abstracts;

namespace BrainMate.Service.Implementations
{
	public class EventService : IEventService
	{
		#region Fields
		private readonly IUnitOfWork _unitOfWork;
		#endregion
		#region Constructors
		public EventService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		#endregion
		#region Handle Functions

		public IQueryable<Event> FilterEventsPaginatedQueryable()
		{
			var queryable = _unitOfWork.events.GetTableNoTracking().OrderBy(x => x.Time).AsQueryable();
			return queryable;
		}

		public IQueryable<Event> FilterEventsSearchQueryable(string search)
		{
			var SearchString = _unitOfWork.events.GetTableNoTracking().Where(x => x.TaskEn == search || x.TaskAr == search).AsQueryable();
			return SearchString;
		}

		public async Task<Event> GetEventByIdAsync(int id)
		{
			var Event = await _unitOfWork.events.GetByIdAsync(id);
			return Event;

		}
		public async Task<string> AddAsync(Event Event)
		{
			var ExistEvent = _unitOfWork.events.
				GetTableNoTracking()
				.Where(x => x.TaskEn!.Equals(Event.TaskEn))
				.FirstOrDefault();
			if (ExistEvent != null) return "Exist";
			// Add
			try
			{
				await _unitOfWork.events.AddAsync(Event);
				return "Success";
			}
			catch (Exception)
			{
				return "FailedToAdd";
			}
		}

		public async Task<string> UpdateAsync(Event Event)
		{
			try
			{
				await _unitOfWork.events.UpdateAsync(Event);
				return "Success";
			}
			catch
			{
				return "FailedToUpdate";
			}
		}

		public async Task<string> DeleteAsync(Event Event)
		{
			var transaction = await _unitOfWork.events.BeginTransactionAsync();
			try
			{
				await _unitOfWork.events.DeleteAsync(Event);
				await transaction.CommitAsync();
				return "Success";
			}
			catch
			{
				await transaction.RollbackAsync();
				return "Failed";
			}
		}

		#endregion
	}
}
