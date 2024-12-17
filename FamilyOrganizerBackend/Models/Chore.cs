namespace FamilyOrganizerBackend.Models
{
    public class Chore
    { 
        public int Id { get; set; } 
        public string Description { get; set; } 
        public string AssignedTo { get; set; } 
        public DateTime DueDate { get; set; } 
    }
}
