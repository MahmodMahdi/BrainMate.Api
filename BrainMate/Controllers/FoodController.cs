using BrainMate.Api.Base;
using BrainMate.Core.Features.Foods.Queries.Models;
using BrainMate.Data.Routing;
using Microsoft.AspNetCore.Mvc;

namespace BrainMate.Api.Controllers
{
	[ApiController]
	public class FoodController : AppControllerBase
	{
		[HttpGet(Routing.FoodRouting.GetAll)]
		public async Task<IActionResult> GetInstructorList()
		{
			var foods = NewResult(await _mediator.Send(new GetFoodListQuery()));
			return foods;
		}
	}
}
