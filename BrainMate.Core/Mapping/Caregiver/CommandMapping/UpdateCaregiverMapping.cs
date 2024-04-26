using BrainMate.Core.Features.Caregiver.Commands.Models;
using BrainMate.Data.Entities.Identity;
namespace BrainMate.Core.Mapping.Caregiver
{
    public partial class CaregiverProfile
    {
        public void UpdateCaregiverCommandMapping()
        {
            CreateMap<UpdateCaregiverCommand, Patient>()
                .ForMember(dest => dest.Image, op => op.Ignore());

        }
    }
}
