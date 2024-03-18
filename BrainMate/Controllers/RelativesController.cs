using BrainMate.Api.Base;
using BrainMate.Core.Features.Relatives.Queries.Models;
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
			var relative = Ok(await _mediator.Send(new GetRelativesByIdQuery(id)));
			return relative;
		}
	}
}
