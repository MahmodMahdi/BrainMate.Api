using BrainMate.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BrainMate.Core.Features.Relative.Commands.Models
{
	public class UpdateRelativeCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
		public string? NameAr { get; set; }
		public string? NameEn { get; set; }
		public string? Address { get; set; }
		public int Age { get; set; }
		[RegularExpression("01[0125][0-9]{8}", ErrorMessage = "Enter Valid Phone Number.")]
		public string? Phone { get; set; }
		public IFormFile? Image { get; set; }
		public string? Job { get; set; }
		public string? Description { get; set; }
		public string? RelationShip { get; set; }
		public int RelationShipDegree { get; set; }
		//public int? PatientId { get; set; }
	}
}
