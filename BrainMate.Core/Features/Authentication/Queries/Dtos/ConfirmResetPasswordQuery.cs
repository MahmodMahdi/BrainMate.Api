
using BrainMate.Core.Bases;
using MediatR;

namespace BrainMate.Core.Features.Authentication.Queries.Dtos
{
	public class ConfirmResetPasswordQuery : IRequest<Response<string>>
	{
		public string? Code { get; set; }
		public string? Email { get; set; }
	}
}
