using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TennisClub_0._1.Requests;

public class TournamentRequest
{
    public int Id { get; set; }

    public int MaxMembers { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
    public int Price { get; set; }

    public DateTime StartDateTime { get; set; }
    
    public List<int>? SelectedCourtIds { get; set; }
    
    public List<SelectListItem>? CourtOptions { get; set; }
    
    public IFormFile Image { get; set; }
}