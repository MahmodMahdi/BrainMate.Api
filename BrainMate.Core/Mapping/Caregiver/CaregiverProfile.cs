using AutoMapper;

namespace BrainMate.Core.Mapping.Caregiver
{
    public partial class CaregiverProfile : Profile
    {
        public CaregiverProfile()
        {
            GetCaregiverByIdMapping();
            UpdateCaregiverCommandMapping();
        }
    }
}
