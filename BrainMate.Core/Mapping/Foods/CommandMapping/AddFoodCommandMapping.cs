using BrainMate.Core.Features.Foods.Commands.Models;
using BrainMate.Data.Entities;

namespace BrainMate.Core.Mapping.Foods
{
	public partial class FoodMapping
	{
		public void AddFoodCommandMapping()
		{
			CreateMap<AddFoodCommand, Food>()
			.ForMember(dest => dest.Image, op => op.Ignore());
		}
	}
}
