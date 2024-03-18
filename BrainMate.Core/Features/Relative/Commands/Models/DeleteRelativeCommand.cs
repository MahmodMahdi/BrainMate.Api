using BrainMate.Core.Bases;
using MediatR;

namespace BrainMate.Core.Features.Relative.Commands.Models
{
	public class DeleteRelativeCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
		public DeleteRelativeCommand(int id)
		{
			Id = id;
		}
	}
}
