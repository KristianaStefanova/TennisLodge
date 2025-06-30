using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TennisLodge.Data.Models;

namespace TennisLodge.Data
{
    public class TennisLodgeDbContext : IdentityDbContext<ApplicationUser>
    {
        public TennisLodgeDbContext(DbContextOptions<TennisLodgeDbContext> options)
            : base(options)
        {
        }

        public DbSet<AccommodationRequest> AccommodationRequests { get; set; } = null!;
        public DbSet<Accommodation> Accommodations { get; set; } = null!;
        public DbSet<PlayerProfile> PlayerProfiles { get; set; } = null!;
        public DbSet<Tournament> Tournaments { get; set; } = null!;
        public DbSet<UserTournament> UserTournaments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AccommodationRequest>()
                .HasOne(ar => ar.Tournament)
                .WithMany(t => t.AccommodationRequests)
                .HasForeignKey(ar => ar.TournamentId)
                .OnDelete(DeleteBehavior.SetNull);


            builder.ApplyConfigurationsFromAssembly(typeof(TennisLodgeDbContext).Assembly);
        }
    }
}
