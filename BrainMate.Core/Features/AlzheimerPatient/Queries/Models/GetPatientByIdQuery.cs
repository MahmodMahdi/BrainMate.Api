using BrainMate.Core.Bases;
using BrainMate.Core.Features.AlzheimerPatient.Queries.Dto;
using MediatR;

namespace BrainMate.Core.Features.AlzheimerPatient.Queries.Models
{
	public class GetPatientByIdQuery : IRequest<Response<GetPatientResponse>>
	{
		public int Id { get; set; }
		public GetPatientByIdQuery(int id)
		{
			Id = id;
		}
	}
}
