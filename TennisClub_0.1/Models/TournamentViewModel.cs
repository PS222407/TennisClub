using Microsoft.AspNetCore.Mvc.Rendering;

namespace TennisClub_0._1.Models;

public class TournamentViewModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Price { get; set; }

    public int MaxMembers { get; set; }

    public DateTime StartDateTime { get; set; }

    //TODO viewmodel
    public List<User>? Participants { get; set; }
    
    public List<CourtViewModel>? Courts { get; set; }
    
    public List<int>? SelectedCourtIds { get; set; }
    
    public List<SelectListItem>? CourtOptions { get; set; }
}