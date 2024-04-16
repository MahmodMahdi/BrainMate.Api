using BrainMate.Core.Wrappers;
using BrainMate.Data.Entities;
using BrainMate.XUnitTest.Wrappers.Interfaces;

namespace BrainMate.XUnitTest.Wrappers.Implementation
{
	public class PaginatedService
	{
		#region Event Paginated
		public class EventPaginatedService : IPaginatedService<Event>
		{
			public Task<PaginateResult<Event>> ReturnPaginatedResult(IQueryable<Event> source, int PageNumber, int PageSize)
			{
				return source.ToPaginatedListAsync(PageNumber, PageSize);
			}
		}
		#endregion
		#region Relative Paginated
		public class RelativePaginatedService : IPaginatedService<Relatives>
		{
			public Task<PaginateResult<Relatives>> ReturnPaginatedResult(IQueryable<Relatives> source, int PageNumber, int PageSize)
			{
				return source.ToPaginatedListAsync(PageNumber, PageSize);
			}
		}
		#endregion
		#region Event Paginated
		public class MedicinePaginatedService : IPaginatedService<Medicine>
		{
			public Task<PaginateResult<Medicine>> ReturnPaginatedResult(IQueryable<Medicine> source, int PageNumber, int PageSize)
			{
				return source.ToPaginatedListAsync(PageNumber, PageSize);
			}
		}
		#endregion
	}
}
