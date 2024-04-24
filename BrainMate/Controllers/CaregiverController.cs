using BrainMate.Api.Base;
using BrainMate.Core.Features.Caregiver.Commands.Models;
using BrainMate.Core.Features.Caregiver.Queries.Models;
using BrainMate.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BrainMate.Api.Controllers
{
    [ApiController]
    public class CaregiverController : AppControllerBase
    {
        [Authorize]
        [SwaggerOperation(Summary = "بيانات الشخص المرافق على هذا المريض", OperationId = "CaregiverProfile")]
        [HttpGet(Routing.CaregiverRouting.Profile)]
        public async Task<IActionResult> CaregiverProfile()
        {
            var caregiver = NewResult(await _mediator.Send(new GetCaregiverQuery()));
            return caregiver;
        }

        [Authorize(Roles = "Caregiver")]
        [HttpPut(Routing.CaregiverRouting.Update)]
        [SwaggerOperation(Summary = "تعديل بيانات المريض", OperationId = "UpdateCaregiver")]
        public async Task<IActionResult> UpdateCaregiver([FromForm] UpdateCaregiverCommand command)
        {
            var result = NewResult(await _mediator.Send(command));
            return result;
        }
        [Authorize(Roles = "Caregiver")]
        [SwaggerOperation(Summary = "حذف حساب المرافق علي المستخدم ", OperationId = "Delete")]
        [HttpDelete(Routing.CaregiverRouting.Delete)]
        public async Task<IActionResult> DeleteCaregiverAccount()
        {
            var user = NewResult(await _mediator.Send(new DeleteCaregiverCommand()));
            return user;
        }
    }
}
