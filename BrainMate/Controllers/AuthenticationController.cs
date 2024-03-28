using BrainMate.Api.Base;
using BrainMate.Core.Features.Authentication.Commands.Models;
using BrainMate.Core.Features.Authentication.Queries.Dtos;
using BrainMate.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrainMate.Api.Controllers
{
	[ApiController]
	public class AuthenticationController : AppControllerBase
	{
		[HttpPost(Routing.AuthenticationRouting.SignIn)]
		public async Task<IActionResult> SignIn([FromForm] SignInCommand command)
		{
			var patient = NewResult(await _mediator.Send(command));
			return patient;
		}
		[Authorize]
		[HttpGet(Routing.AuthenticationRouting.ConfirmEmail)]
		public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
		{
			var email = NewResult(await _mediator.Send(query));
			return email;
		}
	}
}
