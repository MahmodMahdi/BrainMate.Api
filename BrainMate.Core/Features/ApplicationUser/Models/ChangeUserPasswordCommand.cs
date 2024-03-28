using BrainMate.Core.Bases;
using MediatR;

namespace BrainMate.Core.Features.ApplicationUser.Models
{
	public class ChangeUserPasswordCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
		public string? CurrentPassword { get; set; }
		public string? NewPassword { get; set; }
		public string? ConfirmPassword { get; set; }
	}
}
