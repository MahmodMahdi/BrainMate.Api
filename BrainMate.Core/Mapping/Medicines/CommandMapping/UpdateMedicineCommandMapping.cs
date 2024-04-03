using BrainMate.Core.Features.Medicines.Commands.Models;
using BrainMate.Data.Entities;

namespace BrainMate.Core.Mapping.Medicines;
public partial class MedicineProfile
{
	public void UpdateMedicineCommandMapping()
	{
		CreateMap<UpdateMedicineCommand, Medicine>()
				.ForMember(dest => dest.Image, op => op.Ignore());
	}
}
