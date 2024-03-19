using BrainMate.Core.Features.Foods.Commands.Models;
using BrainMate.Data.Entities;

namespace BrainMate.Core.Mapping.Foods;

public partial class FoodMapping
{
	public void UpdateFoodCommandMapping()
	{
		CreateMap<UpdateFoodCommand, Food>()
		.ForMember(dest => dest.Image, op => op.Ignore());
	}
}