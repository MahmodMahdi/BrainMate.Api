using BrainMate.Core.Features.Caregiver.Queries.Dto;
using BrainMate.Data.Entities.Identity;

namespace BrainMate.Core.Mapping.Caregiver;

public partial class CaregiverProfile
{
    public void GetCaregiverByIdMapping()
    {
        CreateMap<Patient, GetCaregiverResponse>();
    }
}