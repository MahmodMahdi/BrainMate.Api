using BrainMate.Core.Features.Relatives.Queries.Dtos;
using BrainMate.Core.Wrappers;
using MediatR;

namespace BrainMate.Core.Features.Relatives.Queries.Models
{
	public class GetRelativesPaginatedListQuery : IRequest<PaginateResult<GetRelativesPaginatedListResponse>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public string? Search { get; set; }
	}
}
