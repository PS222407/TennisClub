using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TennisClub_0._1.Migrations.Models;

namespace TennisClub_0._1.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tournament> Tournament { get; set; } = default!;

    public DbSet<Court> Court { get; set; } = default!;
}