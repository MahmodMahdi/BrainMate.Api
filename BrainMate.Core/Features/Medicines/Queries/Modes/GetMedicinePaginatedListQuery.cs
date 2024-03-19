using BrainMate.Core.Features.Medicines.Queries.Dtos;
using BrainMate.Core.Wrappers;
using MediatR;

namespace BrainMate.Core.Features.Medicines.Queries.Modes
{
	public class GetMedicinePaginatedListQuery : IRequest<PaginateResult<GetMedicinePaginatedListResponse>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	}
}
