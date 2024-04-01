using BrainMate.Core.Bases;
using BrainMate.Core.Features.Events.Queries.Dtos;
using MediatR;

namespace BrainMate.Core.Features.Events.Queries.Models
{
	public class GetEventByIdQuery : IRequest<Response<GetEventResponse>>
	{
		public int Id { get; set; }
		public GetEventByIdQuery(int id)
		{
			Id = id;
		}
	}
}
