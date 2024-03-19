namespace BrainMate.Core.Features.Foods.Queries.Dtos
{
	public class GetFoodListResponse
	{
		public int Id { get; set; }
		public string? Image { get; set; }
		public string? Name { get; set; }
		public string? Type { get; set; }
		public TimeOnly? Time { get; set; }

	}
}
