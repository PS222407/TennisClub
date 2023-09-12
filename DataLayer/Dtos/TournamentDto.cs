namespace DataLayer.Dtos;

public class TournamentDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Price { get; set; }

    public int MaxMembers { get; set; }

    public DateTime StartDateTime { get; set; }

    public List<CourtDto>? Courts { get; set; }

    private List<int>? _courtIds;
    
    public List<int>? CourtIds
    {
        get
        {
            if (_courtIds == null && Courts != null)
            {
                return Courts.Select(court => court.Id).ToList();
            }
            return _courtIds;
        }
        set => _courtIds = value;
    }
}