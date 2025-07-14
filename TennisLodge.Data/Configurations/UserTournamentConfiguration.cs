using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisLodge.Data.Models;

namespace TennisLodge.Data.Configurations
{
    public class UserTournamentConfiguration : IEntityTypeConfiguration<UserTournament>
    {
        public void Configure(EntityTypeBuilder<UserTournament> entity)
        {
            // Defines composite Primary Key of the Mapping Entity
            entity
                .HasKey(ud => new { ud.UserId, ud.TournamentId });


            // Define required constraint for the UserId, as it is type string.
            entity
                .Property(ud => ud.UserId)
                .IsRequired();


            // Define default value for the soft-delete functionality
            entity
                .Property(ud => ud.IsDeleted)
                .HasDefaultValue(false);


            // Configure relations between UserTournament and IdentityUser
            entity
                .HasOne(ud => ud.User)
                .WithMany(u => u.UserTournaments)
                .HasForeignKey(ud => ud.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            //Configure relations between UserTournament and Tournament
            entity
                .HasOne(ud => ud.Tournament)
                .WithMany(d => d.UserTournaments)
                .HasForeignKey(ud => ud.TournamentId)
                .OnDelete(DeleteBehavior.Restrict);


            // Define quiery filter to hide the UserTournament entries reffering deleted Tournament.
            // Solves the problem with relations during delete
            entity
                .HasQueryFilter(ud => ud.Tournament.IsDeleted == false &&
                                      ud.IsDeleted == false);

        }
    }
}
