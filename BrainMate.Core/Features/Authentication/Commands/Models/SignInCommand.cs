using BrainMate.Core.Bases;
using BrainMate.Data.Responses;
using MediatR;

namespace BrainMate.Core.Features.Authentication.Commands.Models
{
	public class SignInCommand : IRequest<Response<JwtAuthenticationResponse>>
	{
		public string? Email { get; set; }
		public string? Password { get; set; }
	}
}
