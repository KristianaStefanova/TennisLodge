using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisLodge.Data.Models;
using static TennisLodge.Data.Common.EntityConstants.AccommodationRequest;
namespace TennisLodge.Data.Configurations
{
    public class AccommodationRequestConfiguration : IEntityTypeConfiguration<AccommodationRequest>
    {
        public void Configure(EntityTypeBuilder<AccommodationRequest> builder)
        {

            builder
                .HasKey(ar => ar.Id);


            builder
                .Property(ar => ar.Id)
                   .ValueGeneratedOnAdd()
                   .HasComment("Primary key of the accommodation request");


            builder
                .Property(ar => ar.GuestUserId)
                   .IsRequired()
                   .HasComment("Foreign key to the user requesting accommodation");


            builder
                .HasOne(ar => ar.GuestUser)
                   .WithMany(u => u.AccommodationRequests)
                   .HasForeignKey(ar => ar.GuestUserId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder
                .Property(ar => ar.TournamentId)
                   .IsRequired()
                   .HasComment("Foreign key to the tournament the request is for");


            builder
                .HasOne(ar => ar.Tournament)
                   .WithMany(t => t.AccommodationRequests)
                   .HasForeignKey(ar => ar.TournamentId)
                   .IsRequired(false) 
                   .OnDelete(DeleteBehavior.Restrict);


            builder
                .Property(ar => ar.NumberOfGuests)
                   .IsRequired();


            builder
                .Property(ar => ar.IsFulfilled)
                   .IsRequired()
                   .HasDefaultValue(false)
                   .HasComment("Indicates whether the request has been fulfilled or matched");


            builder
                .Property(ar => ar.CreatedOn)
                .HasDefaultValueSql("GETUTCDATE()");


            builder
               .Property(ar => ar.Notes)
               .HasMaxLength(NotesdMaxLength) 
               .IsRequired(false)
               .HasComment("Additional notes or requests from the guest");

        }
    }
}
