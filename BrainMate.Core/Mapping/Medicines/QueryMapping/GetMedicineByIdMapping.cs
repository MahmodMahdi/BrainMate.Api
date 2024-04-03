using BrainMate.Core.Features.Medicines.Queries.Dtos;
using BrainMate.Data.Entities;


namespace BrainMate.Core.Mapping.Medicines;
public partial class MedicineProfile
{
	public void GetMedicineByIdMapping()
	{
		CreateMap<Medicine, GetMedicineResponse>()
	   .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.NameAr!, src.NameEn!)));
	}
}