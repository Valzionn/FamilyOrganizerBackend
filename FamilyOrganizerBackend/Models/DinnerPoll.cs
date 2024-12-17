namespace FamilyOrganizerBackend.Models
{
    public class DinnerPoll
    {
        public int Id { get; set; }
        public string Dish { get; set; }
        public DateTime Date { get; set; }
        public List<Vote> Votes { get; set; } = new List<Vote>();
    }
}