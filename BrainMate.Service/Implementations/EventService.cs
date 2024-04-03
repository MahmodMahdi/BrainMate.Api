using BrainMate.Data.Entities;
using BrainMate.Infrastructure.UnitofWork;
using BrainMate.Service.Abstracts;
using Microsoft.EntityFrameworkCore;

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

		public IQueryable<Event> FilterEventsPaginatedQueryable(string search)
		{
			var queryable = _unitOfWork.events.GetTableNoTracking().OrderBy(x => x.Time).AsQueryable();
			if (search != null)
			{
				queryable = queryable.Where(x => x.TaskEn!.Contains(search) || x.TaskAr!.Contains(search));
			}
			return queryable;
		}

		public async Task<Event> GetByIdAsync(int id)
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
		public async Task<bool> IsNameExist(string name)
		{
			var student = await _unitOfWork.events.GetTableNoTracking()
												  .Where(x => x.TaskAr!.Equals(name) || x.TaskEn!.Equals(name))
												  .FirstOrDefaultAsync();
			if (student == null) { return false; }
			else return true;
		}
		public async Task<bool> IsNameExcludeSelf(string name, int id)
		{
			var Department = await _unitOfWork.events.GetTableNoTracking()
										.Where(x => x.TaskEn!.Equals(name) && !x.Id.Equals(id)
											|| x.TaskAr!.Equals(name) && !x.Id.Equals(id))
										.FirstOrDefaultAsync();
			if (Department == null) { return false; }
			else return true;
		}
		#endregion
	}
}
