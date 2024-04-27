namespace BrainMate.Data.Entities
{
    public class Medicine
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public int? Frequency { get; set; }
        public DateOnly? StartAt { get; set; }
        public DateOnly? EndAt { get; set; }
        public string? PatientEmail { get; set; }
        public string? CaregiverEmail { get; set; }
    }
}
