using BrainMate.Core.Bases;
using MediatR;

namespace BrainMate.Core.Features.Caregiver.Commands.Models
{
    public class DeleteCaregiverCommand : IRequest<Response<string>>
    {
    }
}
