using Microsoft.EntityFrameworkCore;
using SportBet.Models;

namespace SportBet.DBContexts
{
    public class MatchContext : DbContext
    {
        public MatchContext(DbContextOptions<MatchContext> options) : base(options)
        { 
        }

        #region Properties

        public DbSet<Match> Matches { get; set; }

        public DbSet<MatchOdd> MatchOdds { get; set; }

        #endregion

        #region Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
                .Property(m => m.MatchDate)
                .HasConversion(
                    v => v.ToDateTime(TimeOnly.MinValue),   // Convert DateOnly to DateTime
                    v => DateOnly.FromDateTime(v));         // Convert DateTime to DateOnly

            modelBuilder.Entity<Match>()
                .Property(m => m.MatchTime)
                .HasConversion(
                    v => v.ToTimeSpan(),                    // Convert TimeOnly to TimeSpan
                    v => TimeOnly.FromTimeSpan(v));         // Convert TimeSpan to TimeOnly
        }

        #endregion
    }
}
