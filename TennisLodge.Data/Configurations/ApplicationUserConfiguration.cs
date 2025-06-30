using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;
using TennisLodge.Data.Models;

namespace TennisLodge.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> entity)
        {
            ApplicationUser adminUser = new ApplicationUser
            {
                Id = "7699db7d-964f-4782-8209-d76562e0fece",
                UserName = "admin@tennislodge.com",
                NormalizedUserName = "ADMIN@TENNISLODGE.COM",
                Email = "admin@tennislodge.com",
                NormalizedEmail = "ADMIN@TENNISLODGE.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = "Admin",
                LastName = "User",
                City = "Sofia"
            };

            var hasher = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin123!");

            entity
                .HasData(adminUser);
        }
    }
}
