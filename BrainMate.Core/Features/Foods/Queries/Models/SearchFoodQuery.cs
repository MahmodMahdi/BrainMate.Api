using BrainMate.Core.Bases;
using BrainMate.Core.Features.Foods.Queries.Dtos;
using MediatR;

namespace BrainMate.Core.Features.Foods.Queries.Models
{
	public class SearchFoodQuery : IRequest<Response<List<SearchFoodResponse>>>
	{
		public string? Search { get; set; }
	}
}
