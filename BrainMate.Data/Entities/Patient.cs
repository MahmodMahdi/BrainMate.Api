using BrainMate.Data.Commons;
using System.ComponentModel.DataAnnotations;

namespace BrainMate.Data.Entities
{
	public class Patient : GeneralLocalizableEntity
	{
		public Patient()
		{
			Relatives = new HashSet<Relatives>();
			Medicines = new HashSet<Medicine>();
			Food = new HashSet<Food>();
		}
		public int Id { get; set; }
		public string? NameAr { get; set; }
		[Display(Name = "Name")]
		public string? NameEn { get; set; }
		public string? Address { get; set; }
		public int Age { get; set; }
		public string? Phone { get; set; }
		public string? Image { get; set; }
		public string? Job { get; set; }
		public virtual ICollection<Relatives>? Relatives { get; set; }
		public virtual ICollection<Medicine>? Medicines { get; set; }
		public virtual ICollection<Food>? Food { get; set; }
	}
}
