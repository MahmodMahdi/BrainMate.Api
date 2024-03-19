namespace BrainMate.Core.Features.Relative.Queries.Dtos
{
	public class GetRelativesResponse
	{
		public int Id { get; set; }
		public string? Image { get; set; }
		public string? Name { get; set; }
		public string? Address { get; set; }
		public int Age { get; set; }
		public string? Phone { get; set; }
		public string? Job { get; set; }
		public string? Description { get; set; }
		public string? RelationShip { get; set; }
		public int RelationShipDegree { get; set; }
	}
}
