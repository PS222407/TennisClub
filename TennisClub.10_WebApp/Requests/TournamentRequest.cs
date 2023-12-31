﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TennisClub_0._1.Validations;

namespace TennisClub_0._1.Requests;

public class TournamentRequest
{
    public int Id { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
    public int MaxMembers { get; set; }

    [Required] public string Name { get; set; }

    [Required] public string Description { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
    public double Price { get; set; }

    [Required] [DateIsInFuture] public DateTime StartDateTime { get; set; }

    [Required] public List<int>? SelectedCourtIds { get; set; }

    public List<SelectListItem>? CourtOptions { get; set; }

    [Required] public IFormFile Image { get; set; }
}