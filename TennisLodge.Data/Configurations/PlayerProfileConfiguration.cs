using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using TennisLodge.Data.Models;
using static TennisLodge.GCommon.ValidationConstatnts.PlayerProfile;

namespace TennisLodge.Data.Configurations
{
    public class PlayerProfileConfiguration : IEntityTypeConfiguration<PlayerProfile>
    {
        public void Configure(EntityTypeBuilder<PlayerProfile> builder)
        {
            builder
                .HasKey(p => p.UserId);


            builder
                .HasOne(p => p.User)
                .WithOne(u => u.PlayerProfile)
                .HasForeignKey<PlayerProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            builder
                .Property(p => p.Nationality)
                .IsRequired()
                .HasMaxLength(NationalityMaxLength)
                .IsRequired();


            builder
                .Property(p => p.PreferredSurface)
                .HasMaxLength(PreferredSurfaceMaxLength)
                .IsRequired(false);


            builder
                .Property(p => p.DominantHand)
                .IsRequired(false)
                .HasMaxLength(DominantHandMaxLength);


        }
    }
}
