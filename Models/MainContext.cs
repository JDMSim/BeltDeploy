using Microsoft.EntityFrameworkCore;
 
namespace ProfessionalProfile.Models
{
    public class MainContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Network> Networks { get; set; }
    }
}