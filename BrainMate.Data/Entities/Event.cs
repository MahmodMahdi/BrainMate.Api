namespace BrainMate.Data.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string? Task { get; set; }
        public TimeOnly? Time { get; set; }
        public string? PatientEmail { get; set; }
        public string? CaregiverEmail { get; set; }
    }
}
