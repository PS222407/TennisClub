using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DataLayer.Dtos;

namespace TennisClub_0._1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<TournamentDto> Tournament { get; set; } = default!;
        
        public DbSet<CourtDto> Court { get; set; } = default!;
    }
}