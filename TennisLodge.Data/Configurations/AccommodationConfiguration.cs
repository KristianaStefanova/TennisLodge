using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisLodge.Data.Models;
using static TennisLodge.Data.Common.EntityConstants.Accommodation;

namespace TennisLodge.Data.Configurations
{
    public class AccommodationConfiguration : IEntityTypeConfiguration<Accommodation>
    {
        public void Configure(EntityTypeBuilder<Accommodation> builder)
        {

            builder
                .HasKey(a => a.Id);


            builder
                .Property(a => a.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Primary key for the Accommodation entity");


            builder
                .Property(a => a.HostUserId)
                .IsRequired()
                .HasComment("Foreign key to the user offering the accommodation");


            builder
                .HasOne(a => a.HostUser)
                .WithMany(u => u.AccommodationsOffered)
                .HasForeignKey(a => a.HostUserId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .Property(a => a.City)
                .IsRequired()
                .HasMaxLength(CityMaxLength)
                .HasComment("City where the accommodation is located");


            builder
                .Property(a => a.Address)
                .IsRequired()
                .HasMaxLength(AddressMaxLength);


            builder
                .Property(a => a.MaxGuests)
                .IsRequired()
                .HasComment("Maximum number of guests that can be hosted");


            builder
                .Property(a => a.IsAvailable)
                .HasDefaultValue(true)
                .HasComment("Indicates if the accommodation is currently available");


            builder
                .Property(a => a.Notes)
                .HasMaxLength(NotesMaxLength)
                .HasComment("Optional description or notes about the accommodation");


            builder
                .Property(a => a.CreatedOn)
                .HasDefaultValueSql("GETUTCDATE()");


            builder
                 .Property(a => a.AvailableFrom)
                 .IsRequired()
                 .HasComment("Start of availability window");


            builder
                .Property(a => a.AvailableTo)
                .IsRequired()
                .HasComment("End of availability window");

        }
    }
}
