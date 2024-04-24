using BrainMate.Core.Bases;
using BrainMate.Core.Features.Caregiver.Queries.Dto;
using MediatR;

namespace BrainMate.Core.Features.Caregiver.Queries.Models
{
    public class GetCaregiverQuery : IRequest<Response<GetCaregiverResponse>>
    {
    }
}
