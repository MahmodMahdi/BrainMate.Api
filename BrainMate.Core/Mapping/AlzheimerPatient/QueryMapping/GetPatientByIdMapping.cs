using AutoMapper;
using BrainMate.Core.Features.AlzheimerPatient.Queries.Dto;
using BrainMate.Data.Entities.Identity;

namespace BrainMate.Core.Mapping.AlzheimerPatient
{
    public partial class PatientProfile : Profile
    {
        public void GetPatientByIdMapping()
        {
            CreateMap<Patient, GetPatientResponse>();
        }
    }
}
