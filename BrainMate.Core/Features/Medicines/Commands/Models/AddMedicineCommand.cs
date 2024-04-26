using BrainMate.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BrainMate.Core.Features.Medicines.Commands.Models
{
    public class AddMedicineCommand : IRequest<Response<string>>
    {
        public string? Name { get; set; }
        public IFormFile? Image { get; set; }
        public int? Frequency { get; set; }
        public DateOnly? StartAt { get; set; }
        public DateOnly? EndAt { get; set; }
    }
}
