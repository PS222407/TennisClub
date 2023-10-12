namespace BusinessLogicLayer.Models;

public class Tournament
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Price { get; set; }

    public int MaxMembers { get; set; }

    public DateTime StartDateTime { get; set; }

    public List<User> Users { get; set; } = new();

    public List<Court> Courts { get; set; } = new();

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

    public string ImageUrl { get; set; }

    public void AddUser(User user)
    {
        if (!Users.Any(u => u.Id == user.Id))
        {
            Users.Add(user);
        }
    }

    public void AddCourt(Court court)
    {
        if (!Courts.Any(c => c.Id == court.Id))
        {
            Courts.Add(court);
        }
    }

    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Name) &&
               !string.IsNullOrWhiteSpace(Description) &&
               Price > 0 &&
               MaxMembers > 0 &&
               StartDateTime > DateTime.Now &&
               !string.IsNullOrEmpty(ImageUrl) &&
               _courtIds != null && _courtIds.Count > 0;
    }
}