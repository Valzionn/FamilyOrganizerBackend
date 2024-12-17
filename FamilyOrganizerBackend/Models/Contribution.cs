namespace FamilyOrganizerBackend.Models
{
    public class Contribution
    {
        public int Id { get; set; }
        public string Member { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}