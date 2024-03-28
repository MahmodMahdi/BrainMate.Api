using BrainMate.Api.Base;
using BrainMate.Core.Features.ApplicationUser.Commands.Models;
using BrainMate.Core.Features.ApplicationUser.Models;
using BrainMate.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrainMate.Api.Controllers
{
	[ApiController]
	public class ApplicationUserController : AppControllerBase
	{
		[Authorize]
		[HttpPost(Routing.UserRouting.Create)]
		public async Task<IActionResult> Register([FromBody] RegisterCommand command)
		{
			var user = NewResult(await _mediator.Send(command));
			return user;
		}
		[Authorize]
		[HttpPut(Routing.UserRouting.Update)]
		public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
		{
			var user = NewResult(await _mediator.Send(command));
			return user;
		}
		[Authorize]
		[HttpDelete(Routing.UserRouting.Delete)]
		public async Task<IActionResult> Delete([FromRoute] int Id)
		{
			var user = NewResult(await _mediator.Send(new DeleteUserCommand(Id)));
			return user;
		}
		[HttpPut(Routing.UserRouting.ChangePassword)]
		public async Task<IActionResult> Update([FromBody] ChangeUserPasswordCommand command)
		{
			var ChangePassword = NewResult(await _mediator.Send(command));
			return ChangePassword;
		}
	}
}
