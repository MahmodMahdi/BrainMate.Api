using BrainMate.Core.Features.Medicines.Queries.Dtos;
using BrainMate.Data.Entities;


namespace BrainMate.Core.Mapping.Medicines
{
	public partial class MedicineMapping
	{
		public void GetMedicinePaginationMapping()
		{
			CreateMap<Medicine, GetMedicinePaginatedListResponse>()
		   .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.NameAr!, src.NameEn!)));
		}
	}
}
