using BrainMate.Core.Bases;
using MediatR;

namespace BrainMate.Core.Features.Authentication.Queries.Dtos
{
	public class LogoutQuery : IRequest<Response<string>>
	{
	}
}
