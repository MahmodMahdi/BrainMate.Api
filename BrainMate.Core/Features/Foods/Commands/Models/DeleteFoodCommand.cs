using BrainMate.Core.Bases;
using MediatR;

namespace BrainMate.Core.Features.Foods.Commands.Models
{
	public class DeleteFoodCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
		public DeleteFoodCommand(int id)
		{
			Id = id;
		}
	}
}
