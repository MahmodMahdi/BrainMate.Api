using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainMate.Data.Entities.Identity
{
    public class PatientRefreshToken
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime ExpireDate { get; set; }
        [ForeignKey(nameof(PatientId))]
        [InverseProperty("PatientRefreshTokens")]
        public virtual Patient? Patient { get; set; }
    }
}
