using BrainMate.Api.Base;
using BrainMate.Core.Features.Foods.Commands.Models;
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
		[HttpGet(Routing.FoodRouting.GetById)]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var food = NewResult(await _mediator.Send(new GetFoodByIdQuery(id)));
			return food;
		}
		[HttpPost(Routing.FoodRouting.Create)]
		public async Task<IActionResult> Create([FromForm] AddFoodCommand command)
		{
			var result = NewResult(await _mediator.Send(command));
			return result;
		}
	}
}
