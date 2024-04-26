using AutoMapper;

namespace BrainMate.Core.Mapping.AlzheimerPatient
{
    public partial class PatientProfile : Profile
    {
        public PatientProfile()
        {
            GetPatientByIdMapping();
            UpdatePatientCommandMapping();

        }
    }
}
