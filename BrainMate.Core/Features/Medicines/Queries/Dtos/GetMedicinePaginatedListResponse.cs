namespace BrainMate.Core.Features.Medicines.Queries.Dtos
{
	public class GetMedicinePaginatedListResponse
	{
		public int Id { get; set; }
		public string? Image { get; set; }
		public string? Name { get; set; }

		public int? Frequency { get; set; }
		public DateOnly? StartAt { get; set; }
		public DateOnly? EndAt { get; set; }
	}
}
