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

        public virtual DbSet<AccommodationRequest> AccommodationRequests { get; set; } = null!;
        public virtual DbSet<Accommodation> Accommodations { get; set; } = null!;
        public virtual DbSet<PlayerProfile> PlayerProfiles { get; set; } = null!;
        public virtual DbSet<Tournament> Tournaments { get; set; } = null!;
        public virtual DbSet<UserTournament> UserTournaments { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<TournamentEntry> TournamentEntries { get; set; } = null!;
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;


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
