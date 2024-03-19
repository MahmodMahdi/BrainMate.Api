using BrainMate.Core.Bases;
using BrainMate.Core.Features.Medicines.Queries.Dtos;
using MediatR;

namespace BrainMate.Core.Features.Medicines.Queries.Modes
{
	public class SearchMedicineQuery : IRequest<Response<List<SearchMedicineResponse>>>
	{
		public string? Search { get; set; }
	}
}
