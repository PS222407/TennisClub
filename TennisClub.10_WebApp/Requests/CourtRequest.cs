using System.ComponentModel.DataAnnotations;

namespace TennisClub_0._1.Requests;

public class CourtRequest
{
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Number must be a non-negative value.")]
    public int Number { get; set; }

    public bool Indoor { get; set; }

    public bool Double { get; set; }
}