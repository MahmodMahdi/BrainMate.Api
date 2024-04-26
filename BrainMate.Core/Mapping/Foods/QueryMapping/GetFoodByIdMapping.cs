using BrainMate.Core.Features.Foods.Queries.Dtos;
using BrainMate.Data.Entities;

namespace BrainMate.Core.Mapping.Foods
{
    public partial class FoodProfile
    {
        public void GetFoodByIdMapping()
        {
            CreateMap<Food, GetFoodResponse>();
        }
    }
}
