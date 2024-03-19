using BrainMate.Api.Base;
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
	}
}
