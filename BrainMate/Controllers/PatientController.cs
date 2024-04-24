using BrainMate.Api.Base;
using BrainMate.Core.Features.AlzheimerPatient.Commands.Models;
using BrainMate.Core.Features.AlzheimerPatient.Queries.Models;
using BrainMate.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BrainMate.Api.Controllers
{
    [ApiController]
    public class PatientController : AppControllerBase
    {
        [Authorize]
        [SwaggerOperation(Summary = "الصفحة الشخصية للمريض", OperationId = "Profile")]
        [HttpGet(Routing.PatientRouting.Profile)]
        public async Task<IActionResult> Profile()
        {
            var patient = NewResult(await _mediator.Send(new GetPatientByIdQuery()));
            return patient;
        }
        [Authorize(Roles = "Patient")]
        [SwaggerOperation(Summary = "Caregiver قفل وفتح حساب ", OperationId = "LockUnlock")]
        [HttpGet(Routing.PatientRouting.LockUnlockCaregiver)]
        public async Task<IActionResult> LockUnlockCaregiver()
        {
            var user = NewResult(await _mediator.Send(new LockUnlockCaregiverQuery()));
            return user;
        }
        [Authorize(Roles = "Caregiver")]
        [HttpPut(Routing.PatientRouting.Update)]
        [SwaggerOperation(Summary = "تعديل بيانات المريض", OperationId = "Update")]
        public async Task<IActionResult> Update([FromForm] UpdatePatientCommand command)
        {
            var result = NewResult(await _mediator.Send(command));
            return result;
        }
        [Authorize(Roles = "Patient")]
        [SwaggerOperation(Summary = " حذف حساب المستخدم ", OperationId = "Delete")]
        [HttpDelete(Routing.PatientRouting.Delete)]
        public async Task<IActionResult> DeletePatientAccount()
        {
            var user = NewResult(await _mediator.Send(new DeletePatientCommand()));
            return user;
        }
    }
}
