namespace TennisClub_0._1.Models;

public class Tournament
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Price { get; set; }

    public int MaxMembers { get; set; }

    public DateTime StartDateTime { get; set; }

    public required List<Court> Courts { get; set; }
    
    public List<User>? Users { get; set; }

    public string ImageUrl { get; set; }
}