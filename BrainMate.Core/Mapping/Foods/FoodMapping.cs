using AutoMapper;

namespace BrainMate.Core.Mapping.Foods
{
	public partial class FoodMapping : Profile
	{
		public FoodMapping()
		{
			GetFoodListMapping();
			GetFoodByIdMapping();
			AddFoodCommandMapping();
			UpdateFoodCommandMapping();
		}
	}
}
