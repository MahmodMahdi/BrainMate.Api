namespace BrainMate.Data.Entities
{
    public class Food
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public TimeOnly? Time { get; set; }
        public string? Image { get; set; }
        public string? PatientEmail { get; set; }
        public string? CaregiverEmail { get; set; }
    }
}
