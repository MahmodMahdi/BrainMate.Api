using BrainMate.Core.Bases;
using MediatR;

namespace BrainMate.Core.Features.AlzheimerPatient.Commands.Models
{
    public class DeletePatientCommand : IRequest<Response<string>> { }
}
