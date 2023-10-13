using Microsoft.AspNetCore.Identity;

namespace TennisClub_0._1.Migrations.Models;

public class User : IdentityUser
{
    public List<Tournament>? Tournaments { get; set; }
}