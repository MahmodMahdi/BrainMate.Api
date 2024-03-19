using BrainMate.Core.Bases;
using BrainMate.Core.Features.Medicines.Queries.Dtos;
using MediatR;

namespace BrainMate.Core.Features.Medicines.Queries.Modes
{
	public class GetMedicineByIdQuery : IRequest<Response<GetMedicineResponse>>
	{
		public int Id { get; set; }
		public GetMedicineByIdQuery(int id)
		{
			Id = id;
		}
	}
}
