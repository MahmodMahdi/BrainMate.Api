using BrainMate.Core.Bases;
using MediatR;

namespace BrainMate.Core.Features.Events.Commands.Models
{
    public class AddEventCommand : IRequest<Response<string>>
    {
        public string? Task { get; set; }
        public TimeOnly? Time { get; set; }
    }
}
