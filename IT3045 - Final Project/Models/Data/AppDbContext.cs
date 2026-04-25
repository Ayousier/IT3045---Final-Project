using Microsoft.EntityFrameworkCore;
using IT3045___Final_Project.Models;

namespace IT3045___Final_Project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<FavoriteMovie> FavoriteMovies { get; set; }
    }
}
