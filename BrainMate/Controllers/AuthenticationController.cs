using BrainMate.Api.Base;
using BrainMate.Core.Features.Authentication.Commands.Models;
using BrainMate.Core.Features.Authentication.Queries.Dtos;
using BrainMate.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BrainMate.Api.Controllers
{
	[ApiController]
	public class AuthenticationController : AppControllerBase
	{
		[SwaggerOperation(Summary = " تسجيل دخول ", OperationId = "SignIn")]
		[HttpPost(Routing.AuthenticationRouting.SignIn)]
		public async Task<IActionResult> SignIn([FromForm] SignInCommand command)
		{
			var patient = NewResult(await _mediator.Send(command));
			return patient;
		}
		[Authorize]
		[SwaggerOperation(Summary = " تأكيد الإيميل ", OperationId = "ConfirmEmail")]
		[HttpGet(Routing.AuthenticationRouting.ConfirmEmail)]
		public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
		{
			var email = NewResult(await _mediator.Send(query));
			return email;
		}
		[SwaggerOperation(Summary = " تأكيد كود الباسورد ", OperationId = "ConfirmResetPassword")]
		[HttpGet(Routing.AuthenticationRouting.ConfirmResetPassword)]
		public async Task<IActionResult> ConfirmResetPassword([FromQuery] ConfirmResetPasswordQuery query)
		{
			var response = NewResult(await _mediator.Send(query));
			return response;
		}
		[SwaggerOperation(Summary = "إرسال كود تأكيد الباسورد", OperationId = "SendResetPasswordCode")]
		[HttpPost(Routing.AuthenticationRouting.SendResetPasswordCode)]
		public async Task<IActionResult> SendResetPasswordCode([FromQuery] SendResetPasswordCommand command)
		{
			var response = NewResult(await _mediator.Send(command));
			return response;
		}
		[SwaggerOperation(Summary = " تغيير الباسورد ", OperationId = "ResetPassword")]
		[HttpPost(Routing.AuthenticationRouting.ResetPassword)]
		public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand command)
		{
			var response = NewResult(await _mediator.Send(command));
			return response;
		}
		[SwaggerOperation(Summary = "آخر إذا كان منتهي Token وإعطاء  Token التحقق من", OperationId = "SendResetPasswordCode")]
		[HttpPost(Routing.AuthenticationRouting.RefreshToken)]
		public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
		{
			var user = NewResult(await _mediator.Send(command));
			return user;
		}
		[SwaggerOperation(Summary = "Token التحقق من صلاحية", OperationId = "SendResetPasswordCode")]
		[HttpPost(Routing.AuthenticationRouting.ValidateToken)]
		public async Task<IActionResult> ValidateToken([FromForm] AuthorizeUserQuery command)
		{
			var user = NewResult(await _mediator.Send(command));
			return user;
		}
	}
}
