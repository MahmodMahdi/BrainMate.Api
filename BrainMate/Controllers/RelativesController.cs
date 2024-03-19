using BrainMate.Api.Base;
using BrainMate.Core.Features.Relative.Commands.Models;
using BrainMate.Core.Features.Relative.Queries.Models;
using BrainMate.Data.Routing;
using Microsoft.AspNetCore.Mvc;

namespace BrainMate.Api.Controllers
{
	[ApiController]
	public class RelativesController : AppControllerBase
	{
		[HttpGet(Routing.RelativesRouting.Paginated)]
		public async Task<IActionResult> PaginatedList([FromQuery] GetRelativesPaginatedListQuery query)
		{
			var relatives = Ok(await _mediator.Send(query));
			return relatives;
		}
		[HttpGet(Routing.RelativesRouting.GetById)]
		public async Task<IActionResult> GetRelativeById([FromRoute] int id)
		{
			var relative = NewResult(await _mediator.Send(new GetRelativesByIdQuery(id)));
			return relative;
		}
		[HttpGet(Routing.RelativesRouting.Search)]
		public async Task<IActionResult> Search([FromQuery] SearchRelativesQuery query)
		{
			var relatives = Ok(await _mediator.Send(query));
			return relatives;
		}
		[HttpPost(Routing.RelativesRouting.Create)]
		public async Task<IActionResult> Create([FromForm] AddRelativeCommand command)
		{
			var result = NewResult(await _mediator.Send(command));
			return result;
		}
		[HttpPut(Routing.RelativesRouting.Update)]
		public async Task<IActionResult> Update([FromForm] UpdateRelativeCommand command)
		{
			var result = NewResult(await _mediator.Send(command));
			return result;
		}
		[HttpDelete(Routing.RelativesRouting.Delete)]
		public async Task<IActionResult> Delete([FromRoute] int Id)
		{
			var result = NewResult(await _mediator.Send(new DeleteRelativeCommand(Id)));
			return result;
		}

	}
}
