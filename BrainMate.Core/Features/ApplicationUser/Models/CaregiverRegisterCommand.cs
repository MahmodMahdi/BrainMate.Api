using BrainMate.Core.Bases;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BrainMate.Core.Features.ApplicationUser.Models
{
    public class CaregiverRegisterCommand : IRequest<Response<string>>
    {
        public string? Name { get; set; }
        [RegularExpression("[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{3}", ErrorMessage = "Enter valid Email")]
        public string? Email { get; set; }
        // [RegularExpression("\\.[A-Za-z]{3}+@[A-Za-z0-9.-]+[A-Z0-9a-z._%+-]", ErrorMessage = "Enter valid Email")]
        public string? PatientEmail { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
