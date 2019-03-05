using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCourse.Models;

namespace AspCourse.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Location> Locations { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TournamentPlayer>()
                .HasKey(ww => new { ww.Id, ww.PlayerId, ww.TournamentId });
            modelBuilder.Entity<TournamentLocation>()
                .HasKey(ww => new { ww.Id, ww.LocationId, ww.TournamentId });
        }

        public DbSet<AspCourse.Models.TournamentPlayer> TournamentPlayer { get; set; }

        public DbSet<AspCourse.Models.TournamentLocation> TournamentLocation { get; set; }
    }
}
