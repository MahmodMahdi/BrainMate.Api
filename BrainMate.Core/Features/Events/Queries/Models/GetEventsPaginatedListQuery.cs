using BrainMate.Core.Features.Events.Queries.Dtos;
using BrainMate.Core.Wrappers;
using MediatR;

namespace BrainMate.Core.Features.Events.Queries.Models
{
	public class GetEventsPaginatedListQuery : IRequest<PaginateResult<GetEventsPaginatedListResponse>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	}
}
