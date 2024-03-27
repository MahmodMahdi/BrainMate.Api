using BrainMate.Api.Base;
using BrainMate.Core.Features.ApplicationUser.Models;
using BrainMate.Data.Routing;
using Microsoft.AspNetCore.Mvc;

namespace BrainMate.Api.Controllers
{
	[ApiController]
	public class ApplicationUserController : AppControllerBase
	{
		[HttpPost(Routing.UserRouting.Create)]
		public async Task<IActionResult> Create([FromBody] AddUserCommand command)
		{
			var user = NewResult(await _mediator.Send(command));
			return user;
		}
	}
}
