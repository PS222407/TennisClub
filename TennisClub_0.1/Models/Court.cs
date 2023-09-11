namespace TennisClub_0._1.Models;

public class Court
{
    public int Id { get; set; }

    public int Number { get; set; }

    public bool Indoor { get; set; }

    public bool Double { get; set; }

    public List<Tournament>? Tournaments { get; set; }
}