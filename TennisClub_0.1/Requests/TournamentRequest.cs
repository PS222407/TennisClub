namespace TennisClub_0._1.Requests;

public class TournamentRequest
{
    public int Id { get; set; }

    public int MaxMembers { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Price { get; set; }

    public DateTime StartDateTime { get; set; }
}