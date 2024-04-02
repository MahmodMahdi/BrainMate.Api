using BrainMate.Api.Base;
using BrainMate.Core.Features.Events.Commands.Models;
using BrainMate.Core.Features.Events.Queries.Models;
using BrainMate.Data.Routing;
using Microsoft.AspNetCore.Mvc;

namespace BrainMate.Api.Controllers
{
	[ApiController]
	public class EventController : AppControllerBase
	{
		[HttpGet(Routing.EventRouting.Paginated)]
		public async Task<IActionResult> PaginatedList([FromQuery] GetEventsPaginatedListQuery query)
		{
			var events = Ok(await _mediator.Send(query));
			return events;
		}
		[HttpGet(Routing.EventRouting.GetById)]
		public async Task<IActionResult> GetEventById([FromRoute] int id)
		{
			var Event = NewResult(await _mediator.Send(new GetEventByIdQuery(id)));
			return Event;
		}
		[HttpPost(Routing.EventRouting.Create)]
		public async Task<IActionResult> Create([FromForm] AddEventCommand command)
		{
			var result = NewResult(await _mediator.Send(command));
			return result;
		}
		[HttpPut(Routing.EventRouting.Update)]
		public async Task<IActionResult> Update([FromForm] UpdateEventCommand command)
		{
			var result = NewResult(await _mediator.Send(command));
			return result;
		}
		[HttpDelete(Routing.EventRouting.Delete)]
		public async Task<IActionResult> Delete([FromRoute] int Id)
		{
			var result = NewResult(await _mediator.Send(new DeleteEventCommand(Id)));
			return result;
		}
	}
}
