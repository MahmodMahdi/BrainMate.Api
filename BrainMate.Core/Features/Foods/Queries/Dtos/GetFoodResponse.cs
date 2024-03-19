namespace BrainMate.Core.Features.Foods.Queries.Dtos
{
	public class GetFoodResponse
	{
		public int Id { get; set; }
		public string? Image { get; set; }
		public string? Name { get; set; }
		public string? Type { get; set; }
		public TimeOnly? Time { get; set; }

	}
}
