namespace BrainMate.Core.Features.Events.Queries.Dtos
{
	public class GetEventResponse
	{
		public int Id { get; set; }
		public string? Task { get; set; }
		public TimeOnly? Time { get; set; }
	}
}
