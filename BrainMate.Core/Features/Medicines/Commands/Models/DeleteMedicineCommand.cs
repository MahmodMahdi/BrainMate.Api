using BrainMate.Core.Bases;
using MediatR;

namespace BrainMate.Core.Features.Medicines.Commands.Models
{
	public class DeleteMedicineCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
		public DeleteMedicineCommand(int id)
		{
			Id = id;
		}
	}
}
