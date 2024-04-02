using BrainMate.Core.Bases;
using MediatR;
namespace BrainMate.Core.Features.Events.Commands.Models;

public class DeleteEventCommand : IRequest<Response<string>>
{
	public int Id { get; set; }
	public DeleteEventCommand(int id)
	{
		Id = id;
	}
}
