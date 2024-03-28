using BrainMate.Core.Bases;
using BrainMate.Data.Responses;
using MediatR;

namespace BrainMate.Core.Features.Authentication.Commands.Models
{
	public class RefreshTokenCommand : IRequest<Response<JwtAuthenticationResponse>>
	{
		public string? Token { get; set; }
		public string? RefreshToken { get; set; }
	}
}
