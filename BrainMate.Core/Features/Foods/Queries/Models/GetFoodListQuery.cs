using BrainMate.Core.Bases;
using BrainMate.Core.Features.Foods.Queries.Dtos;
using MediatR;

namespace BrainMate.Core.Features.Foods.Queries.Models
{
    public class GetFoodListQuery : IRequest<Response<List<GetFoodListResponse>>>
    {
        public string? search { get; set; }
    }
}
