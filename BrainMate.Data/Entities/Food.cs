using BrainMate.Data.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainMate.Data.Entities
{
	public class Food : GeneralLocalizableEntity
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Type { get; set; }
		public DateTime Date { get; set; }
		public int? PatientId { get; set; }

		[ForeignKey(nameof(PatientId))]
		public virtual Patient? Patient { get; set; }
	}
}
