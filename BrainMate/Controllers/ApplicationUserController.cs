using BrainMate.Api.Base;
using BrainMate.Core.Features.ApplicationUser.Commands.Models;
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
		[HttpPut(Routing.UserRouting.Update)]
		public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
		{
			var user = NewResult(await _mediator.Send(command));
			return user;
		}
		[HttpDelete(Routing.UserRouting.Delete)]
		public async Task<IActionResult> Delete([FromRoute] int Id)
		{
			var user = NewResult(await _mediator.Send(new DeleteUserCommand(Id)));
			return user;
		}
	}
}
