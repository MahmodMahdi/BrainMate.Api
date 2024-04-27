using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BrainMate.Data.Entities.Identity
{
    public class Patient : IdentityUser<int>
    {
        public Patient()
        {
            PatientRefreshTokens = new HashSet<PatientRefreshToken>();
        }
        [Display(Name = "Name")]
        public string? Name { get; set; }
        public string? Address { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? Image { get; set; }
        public string? Job { get; set; }
        public string? PatientEmail { get; set; }
        public string? Code { get; set; }
        public virtual ICollection<PatientRefreshToken>? PatientRefreshTokens { get; set; }
    }
}
