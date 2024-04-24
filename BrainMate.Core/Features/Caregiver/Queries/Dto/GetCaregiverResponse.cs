namespace BrainMate.Core.Features.Caregiver.Queries.Dto
{
    public class GetCaregiverResponse
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? Phone { get; set; }
        public string? Job { get; set; }
    }
}
