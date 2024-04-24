using BrainMate.Core.Bases;
using MediatR;

namespace BrainMate.Core.Features.AlzheimerPatient.Queries.Models
{
    public class LockUnlockCaregiverQuery : IRequest<Response<string>>
    {
    }
}
