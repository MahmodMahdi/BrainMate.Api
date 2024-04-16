using BrainMate.Core.Wrappers;

namespace BrainMate.XUnitTest.Wrappers.Interfaces
{
	public interface IPaginatedService<T>
	{
		public Task<PaginateResult<T>> ReturnPaginatedResult(IQueryable<T> source, int PageNumber, int PageSize);
	}
}
