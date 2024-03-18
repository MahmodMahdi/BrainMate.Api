using BrainMate.Api.Base;
using BrainMate.Core.Features.AlzheimerPatient.Commands.Models;
using BrainMate.Core.Features.AlzheimerPatient.Queries.Models;
using BrainMate.Data.Routing;
using Microsoft.AspNetCore.Mvc;

namespace BrainMate.Api.Controllers
{
	[ApiController]
	public class PatientController : AppControllerBase
	{
		[HttpGet(Routing.PatientRouting.GetById)]
		public async Task<IActionResult> GetPatientById([FromRoute] int id)
		{
			var patient = NewResult(await _mediator.Send(new GetPatientByIdQuery(id)));
			return patient;
		}
		[HttpPost(Routing.PatientRouting.Create)]
		public async Task<IActionResult> Create([FromForm] AddPatientCommand command)
		{
			var result = NewResult(await _mediator.Send(command));
			return result;
		}
		[HttpPut(Routing.PatientRouting.Update)]
		public async Task<IActionResult> Update([FromForm] UpdatePatientCommand command)
		{
			var result = NewResult(await _mediator.Send(command));
			return result;
		}
	}
}
