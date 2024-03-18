using BrainMate.Core.Bases;
using BrainMate.Core.Features.Relative.Queries.Dtos;
using MediatR;

namespace BrainMate.Core.Features.Relative.Queries.Models
{
	public class GetRelativesByIdQuery : IRequest<Response<GetRelativesResponse>>
	{
		public int Id { get; set; }
		public GetRelativesByIdQuery(int id)
		{
			Id = id;
		}
	}
}
