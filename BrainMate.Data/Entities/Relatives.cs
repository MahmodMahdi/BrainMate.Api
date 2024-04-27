namespace BrainMate.Data.Entities
{
    public class Relatives
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int? Age { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Image { get; set; }
        public string? Job { get; set; }
        public string? Description { get; set; }
        public string? RelationShip { get; set; }
        public int? RelationShipDegree { get; set; }
        public string? PatientEmail { get; set; }
        public string? CaregiverEmail { get; set; }
    }
}
