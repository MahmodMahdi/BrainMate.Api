using BrainMate.Api.Base;
using BrainMate.Core.Features.ApplicationUser.Commands.Models;
using BrainMate.Core.Features.ApplicationUser.Models;
using BrainMate.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BrainMate.Api.Controllers
{
	[ApiController]
	public class ApplicationUserController : AppControllerBase
	{

		[SwaggerOperation(Summary = " إنشاء حساب جديد ", OperationId = "Register")]
		[HttpPost(Routing.UserRouting.Register)]
		public async Task<IActionResult> Register([FromBody] RegisterCommand command)
		{
			var user = NewResult(await _mediator.Send(command));
			return user;
		}
		[SwaggerOperation(Summary = " إنشاء حساب مرافق جديد ", OperationId = "CaregiverRegister")]
		[HttpPost(Routing.UserRouting.CaregiverRegister)]
		public async Task<IActionResult> CaregiverRegister([FromBody] CaregiverRegisterCommand command)
		{
			var caregiver = NewResult(await _mediator.Send(command));
			return caregiver;
		}
		[Authorize]
		[SwaggerOperation(Summary = " تعديل حساب المستخدم ", OperationId = "Update")]
		[HttpPut(Routing.UserRouting.Update)]
		public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
		{
			var user = NewResult(await _mediator.Send(command));
			return user;
		}
		[Authorize]
		[SwaggerOperation(Summary = " حذف حساب المستخدم ", OperationId = "Delete")]
		[HttpDelete(Routing.UserRouting.Delete)]
		public async Task<IActionResult> Delete([FromRoute] int Id)
		{
			var user = NewResult(await _mediator.Send(new DeleteUserCommand(Id)));
			return user;
		}
		[HttpPut(Routing.UserRouting.ChangePassword)]
		[SwaggerOperation(Summary = " تغيير الباسورد ", OperationId = "ChangePassword")]
		public async Task<IActionResult> Update([FromBody] ChangeUserPasswordCommand command)
		{
			var ChangePassword = NewResult(await _mediator.Send(command));
			return ChangePassword;
		}
	}
}
