using BrainMate.Core.Features.Relative.Queries.Dtos;
using BrainMate.Core.Wrappers;
using MediatR;

namespace BrainMate.Core.Features.Relative.Queries.Models
{
	public class GetRelativesPaginatedListQuery : IRequest<PaginateResult<GetRelativesPaginatedListResponse>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public string? Search { get; set; }
	}
}
