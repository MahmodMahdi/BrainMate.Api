using BrainMate.Core.Bases;
using BrainMate.Core.Features.Foods.Queries.Dtos;
using MediatR;

namespace BrainMate.Core.Features.Foods.Queries.Models
{
	public class GetFoodByIdQuery : IRequest<Response<GetFoodResponse>>
	{
		public int Id { get; set; }
		public GetFoodByIdQuery(int id)
		{
			Id = id;
		}
	}

}
