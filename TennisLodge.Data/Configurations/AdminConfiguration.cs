using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Data.Models;

namespace TennisLodge.Data.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> entity)
        {
            entity
                .HasKey(a => a.Id);

            entity
                .Property(a => a.IsDeleted)
                .HasDefaultValue(false);


            entity
                .HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<Admin>(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasIndex(a => new { a.UserId })
                .IsUnique();

            entity.HasQueryFilter(a => a.IsDeleted == false);
        }
    }
}
