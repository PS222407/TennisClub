using Microsoft.AspNetCore.Mvc.Rendering;

namespace TennisClub_0._1.Models;

public class TournamentViewModel
{
    private double _price;

    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Price
    {
        get => _price;
        set => _price = value / 100;
    }

    public int MaxMembers { get; set; }

    public DateTime StartDateTime { get; set; }

    public List<UserViewModel>? Participants { get; set; }

    public List<CourtViewModel>? Courts { get; set; }

    public List<int>? SelectedCourtIds { get; set; }

    public List<SelectListItem>? CourtOptions { get; set; }

    public string? ImageUrl { get; set; }
}