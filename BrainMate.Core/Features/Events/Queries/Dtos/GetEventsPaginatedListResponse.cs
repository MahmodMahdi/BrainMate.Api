namespace BrainMate.Core.Features.Events.Queries.Dtos
{
    public class GetEventsPaginatedListResponse
    {
        public int Id { get; set; }
        public string? Task { get; set; }
        public TimeOnly? Time { get; set; }
        // public int PatientId { get; set; }
    }
}
