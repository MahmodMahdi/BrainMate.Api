using BrainMate.Api.Base;
using BrainMate.Core.Features.Foods.Commands.Models;
using BrainMate.Core.Features.Foods.Queries.Models;
using BrainMate.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrainMate.Api.Controllers
{
	[ApiController]
	public class FoodController : AppControllerBase
	{
		[Authorize]
		[HttpGet(Routing.FoodRouting.GetAll)]
		public async Task<IActionResult> GetInstructorList()
		{
			var foods = NewResult(await _mediator.Send(new GetFoodListQuery()));
			return foods;
		}
		[Authorize]
		[HttpGet(Routing.FoodRouting.GetById)]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var food = NewResult(await _mediator.Send(new GetFoodByIdQuery(id)));
			return food;
		}
		[Authorize]
		[HttpGet(Routing.FoodRouting.Search)]
		public async Task<IActionResult> Search([FromQuery] SearchFoodQuery query)
		{
			var food = NewResult(await _mediator.Send(query));
			return food;
		}
		[HttpPost(Routing.FoodRouting.Create)]
		public async Task<IActionResult> Create([FromForm] AddFoodCommand command)
		{
			var result = NewResult(await _mediator.Send(command));
			return result;
		}
		[HttpPut(Routing.FoodRouting.Update)]
		public async Task<IActionResult> Update([FromForm] UpdateFoodCommand command)
		{
			var result = NewResult(await _mediator.Send(command));
			return result;
		}
		[HttpDelete(Routing.FoodRouting.Delete)]
		public async Task<IActionResult> Delete([FromRoute] int Id)
		{
			var result = NewResult(await _mediator.Send(new DeleteFoodCommand(Id)));
			return result;
		}
	}
}
