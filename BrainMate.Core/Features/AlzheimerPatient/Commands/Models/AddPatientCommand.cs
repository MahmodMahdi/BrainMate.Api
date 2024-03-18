using BrainMate.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BrainMate.Core.Features.AlzheimerPatient.Commands.Models
{
	public class AddPatientCommand : IRequest<Response<string>>
	{
		public string? NameAr { get; set; }
		public string? NameEn { get; set; }
		public string? Address { get; set; }
		public int Age { get; set; }
		[RegularExpression("01[0125][0-9]{8}", ErrorMessage = "Enter Valid Phone Number.")]
		public string? Phone { get; set; }
		public IFormFile? Image { get; set; }
		public string? Job { get; set; }
	}
}
