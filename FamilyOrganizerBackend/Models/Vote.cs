using FamilyOrganizerBackend.Models;

public class Vote
{
    public int Id { get; set; }
    public int DinnerPollId { get; set; }
    public string Voter { get; set; }
}