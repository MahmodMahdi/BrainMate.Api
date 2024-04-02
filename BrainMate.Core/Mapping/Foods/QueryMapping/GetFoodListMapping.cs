using BrainMate.Core.Features.Foods.Queries.Dtos;
using BrainMate.Data.Entities;

namespace BrainMate.Core.Mapping.Foods
{
	public partial class FoodProfile
	{
		public void GetFoodListMapping()
		{
			CreateMap<Food, GetFoodListResponse>()
		   .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.NameAr!, src.NameEn!)));
		}
	}
}
