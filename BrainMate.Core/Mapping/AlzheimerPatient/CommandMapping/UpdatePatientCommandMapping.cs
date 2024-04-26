using AutoMapper;
using BrainMate.Core.Features.AlzheimerPatient.Commands.Models;
using BrainMate.Data.Entities.Identity;
namespace BrainMate.Core.Mapping.AlzheimerPatient;

public partial class PatientProfile : Profile
{
    public void UpdatePatientCommandMapping()
    {
        CreateMap<UpdatePatientCommand, Patient>()
            .ForMember(dest => dest.Image, op => op.Ignore());

    }
}