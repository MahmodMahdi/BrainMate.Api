using BrainMate.Core.Bases;
using MediatR;

namespace BrainMate.Core.Features.Authentication.Commands.Models
{
	public class SendResetPasswordCommand : IRequest<Response<string>>
	{
		public string? Email { get; set; }
	}
}
