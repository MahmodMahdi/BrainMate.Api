using BrainMate.Core.Bases;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BrainMate.Core.Features.Authentication.Commands.Models
{
	public class ResetPasswordCommand : IRequest<Response<string>>
	{
		public string? Email { get; set; }
		[DataType(DataType.Password)]
		public string? Password { get; set; }
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "The ConfirmPassword not same as Password")]
		public string? ConfirmPassword { get; set; }
	}
}
