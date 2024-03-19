using BrainMate.Data.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainMate.Data.Entities
{
	public class Medicine : GeneralLocalizableEntity
	{
		public int Id { get; set; }
		public string? NameAr { get; set; }
		public string? NameEn { get; set; }
		public string? Image { get; set; }
		public int? Frequency { get; set; }
		public DateOnly? StartAt { get; set; }
		public DateOnly? EndAt { get; set; }
		public int? PatientId { get; set; }

		[ForeignKey(nameof(PatientId))]
		public virtual Patient? Patient { get; set; }
	}
}
