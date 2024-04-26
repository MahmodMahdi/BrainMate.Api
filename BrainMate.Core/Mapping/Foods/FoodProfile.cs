using AutoMapper;

namespace BrainMate.Core.Mapping.Foods
{
    public partial class FoodProfile : Profile
    {
        public FoodProfile()
        {
            GetFoodListMapping();
            GetFoodByIdMapping();
            AddFoodCommandMapping();
            UpdateFoodCommandMapping();
        }
    }
}
