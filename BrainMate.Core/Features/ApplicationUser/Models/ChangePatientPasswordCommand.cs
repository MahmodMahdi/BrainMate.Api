using BrainMate.Core.Bases;
using MediatR;

namespace BrainMate.Core.Features.ApplicationUser.Models
{
    public class ChangePatientPasswordCommand : IRequest<Response<string>>
    {
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
