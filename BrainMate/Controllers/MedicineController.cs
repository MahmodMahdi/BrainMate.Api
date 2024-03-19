using BrainMate.Api.Base;
using BrainMate.Core.Features.Medicines.Commands.Models;
using BrainMate.Core.Features.Medicines.Queries.Modes;
using BrainMate.Data.Routing;
using Microsoft.AspNetCore.Mvc;

namespace BrainMate.Api.Controllers
{
	[ApiController]
	public class MedicineController : AppControllerBase
	{
		[HttpGet(Routing.MedicineRouting.Paginated)]
		public async Task<IActionResult> PaginatedList([FromQuery] GetMedicinePaginatedListQuery query)
		{
			var medicines = Ok(await _mediator.Send(query));
			return medicines;
		}
		[HttpGet(Routing.MedicineRouting.GetById)]
		public async Task<IActionResult> GetMedicineById([FromRoute] int id)
		{
			var medicine = NewResult(await _mediator.Send(new GetMedicineByIdQuery(id)));
			return medicine;
		}
		[HttpGet(Routing.MedicineRouting.Search)]
		public async Task<IActionResult> Search([FromQuery] SearchMedicineQuery query)
		{
			var medicine = NewResult(await _mediator.Send(query));
			return medicine;
		}
		[HttpPost(Routing.MedicineRouting.Create)]
		public async Task<IActionResult> Create([FromForm] AddMedicineCommand command)
		{
			var result = NewResult(await _mediator.Send(command));
			return result;
		}
		[HttpPut(Routing.MedicineRouting.Update)]
		public async Task<IActionResult> Update([FromForm] UpdateMedicineCommand command)
		{
			var result = NewResult(await _mediator.Send(command));
			return result;
		}
		[HttpDelete(Routing.MedicineRouting.Delete)]
		public async Task<IActionResult> Delete([FromRoute] int Id)
		{
			var result = NewResult(await _mediator.Send(new DeleteMedicineCommand(Id)));
			return result;
		}
	}
}
