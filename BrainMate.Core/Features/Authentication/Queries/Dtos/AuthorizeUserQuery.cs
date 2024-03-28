using BrainMate.Core.Bases;
using MediatR;

namespace BrainMate.Core.Features.Authentication.Queries.Dtos
{
	public class AuthorizeUserQuery : IRequest<Response<string>>
	{
		public string? AccessToken { get; set; }
	}
}
