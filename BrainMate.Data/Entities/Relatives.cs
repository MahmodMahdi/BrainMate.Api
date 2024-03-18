using BrainMate.Data.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainMate.Data.Entities
{
	public class Relatives : GeneralLocalizableEntity
	{
		public int Id { get; set; }
		public string? NameAr { get; set; }
		public string? NameEn { get; set; }
		public string? Address { get; set; }
		public int Age { get; set; }
		public string? Phone { get; set; }
		public string? Image { get; set; }
		public string? Job { get; set; }
		public string? Description { get; set; }
		public string? RelationShip { get; set; }
		public int RelationShipDegree { get; set; }
		public int? PatientId { get; set; }

		[ForeignKey(nameof(PatientId))]
		//[InverseProperty("Relatives")]
		public virtual Patient? patient { get; set; }
	}
}
