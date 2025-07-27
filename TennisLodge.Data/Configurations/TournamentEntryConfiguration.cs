using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisLodge.Data.Models;

namespace TennisLodge.Data.Configurations
{
    public class TournamentEntryEntityConfiguration : IEntityTypeConfiguration<TournamentEntry>
    {
        public void Configure(EntityTypeBuilder<TournamentEntry> entity)
        {
            entity
                .HasKey(e => e.Id);
            entity
                .Property(e => e.Id)
                   .HasComment("Primary key of the tournament entry");

            entity
                .Property(e => e.PlayerId)
                   .IsRequired()
                   .HasComment("Foreign key to the player (ApplicationUser)");

            entity
                .HasOne(e => e.Player)
                   .WithMany(u => u.TournamentEntries)
                   .HasForeignKey(e => e.PlayerId)
                   .OnDelete(DeleteBehavior.Restrict);
                   

            entity
                .Property(e => e.TournamentId)
                   .IsRequired()
                   .HasComment("Foreign key to the tournament");

            entity
                .HasOne(e => e.Tournament)
                .WithMany()
                .HasForeignKey(e => e.TournamentId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
    

            entity
                .Property(e => e.RegisteredOn)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .HasComment("Date when the player registered for the tournament");
        }
    }
}
