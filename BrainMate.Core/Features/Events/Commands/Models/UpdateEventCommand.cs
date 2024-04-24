using BrainMate.Core.Bases;
using MediatR;

namespace BrainMate.Core.Features.Events.Commands.Models
{
    public class UpdateEventCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string? Task { get; set; }
        public TimeOnly? Time { get; set; }
    }
}
