using InterviewTest.Models;
using Microsoft.EntityFrameworkCore;
 
namespace InterviewTest.Data
{
    public class Context : DbContext
    {
        
        public Context(DbContextOptions<Context> options) : base(options)
        {}
        public DbSet<Player> Players { get; set; }
        public DbSet<Prize> Prizes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Prize>()
            .HasOne(p => p.Player)
            .WithMany(b => b.Prizes);
        }
    }
}