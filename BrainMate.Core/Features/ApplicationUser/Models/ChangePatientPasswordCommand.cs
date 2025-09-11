using BrainMate.Core.Bases;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BrainMate.Core.Features.ApplicationUser.Models
{
	public class ChangePatientPasswordCommand : IRequest<Response<string>>
	{
		public string? CurrentPassword { get; set; }
		[DataType(DataType.Password)]
		public string? NewPassword { get; set; }
		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "The ConfirmPassword not same as NewPassword")]
		public string? ConfirmPassword { get; set; }
	}
}
