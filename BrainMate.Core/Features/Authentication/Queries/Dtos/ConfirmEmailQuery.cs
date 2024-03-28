using BrainMate.Core.Bases;
using MediatR;
namespace BrainMate.Core.Features.Authentication.Queries.Dtos
{
	public class ConfirmEmailQuery : IRequest<Response<string>>
	{
		public int UserId { get; set; }
		public string? Code { get; set; }
	}
}
