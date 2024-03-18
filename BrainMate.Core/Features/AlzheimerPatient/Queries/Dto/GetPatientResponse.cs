using System.ComponentModel.DataAnnotations;

namespace BrainMate.Core.Features.AlzheimerPatient.Queries.Dto
{
	public class GetPatientResponse
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Address { get; set; }
		[DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateOnly? BirthDate { get; set; }
		public string? Phone { get; set; }
		public string? Image { get; set; }
		public string? Job { get; set; }
	}
}
