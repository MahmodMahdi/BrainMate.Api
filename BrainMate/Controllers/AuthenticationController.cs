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
		public async Task<IActionResult> Create([FromForm] SignInCommand command)
		{
			var user = NewResult(await _mediator.Send(command));
			return user;
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
