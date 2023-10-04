namespace DataLayer.Dtos;

public class TournamentDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Price { get; set; }

    public int MaxMembers { get; set; }

    public DateTime StartDateTime { get; set; }

    public List<UserDto> Users { get; set; } = new();

    public List<CourtDto> Courts { get; set; } = new();

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

    public string? ImageUrl { get; set; }

    public void AddUser(UserDto userDto)
    {
        if (!Users.Any(user => user.Id == userDto.Id))
        {
            Users.Add(userDto);
        }
    }

    public void AddCourt(CourtDto courtDto)
    {
        if (!Courts.Any(court => court.Id == courtDto.Id))
        {
            Courts.Add(courtDto);
        }
    }
}