using BrainMate.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BrainMate.Core.Features.AlzheimerPatient.Commands.Models
{
    public class UpdatePatientCommand : IRequest<Response<string>>
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public DateOnly? BirthDate { get; set; }
        [RegularExpression("01[0125][0-9]{8}", ErrorMessage = "Enter Valid Phone Number.")]
        public string? PhoneNumber { get; set; }
        public IFormFile? Image { get; set; }
        public string? Job { get; set; }
    }
}
