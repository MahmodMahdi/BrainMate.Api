using BrainMate.Core.Features.Foods.Queries.Dtos;
using BrainMate.Data.Entities;

namespace BrainMate.Core.Mapping.Foods
{
	public partial class FoodProfile
	{
		public void GetFoodByIdMapping()
		{
			CreateMap<Food, GetFoodResponse>()
		   .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.NameAr!, src.NameEn!)));
		}
	}
}
