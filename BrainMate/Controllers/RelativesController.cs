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
		public async Task<IActionResult> Paginated([FromQuery] GetRelativesPaginatedListQuery query)
		{
			var relatives = Ok(await _mediator.Send(query));
			return relatives;
		}
	}
}
