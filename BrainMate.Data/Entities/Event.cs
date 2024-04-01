using System.ComponentModel.DataAnnotations.Schema;

namespace BrainMate.Data.Entities
{
	public class Event
	{
		public int Id { get; set; }
		public string? TaskAr { get; set; }
		public string? TaskEn { get; set; }
		public TimeOnly? Time { get; set; }
		public int? PatientId { get; set; }

		[ForeignKey(nameof(PatientId))]
		public Patient? Patient { get; set; }
	}
}
