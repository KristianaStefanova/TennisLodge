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
            entity
                .HasKey(ud => new { ud.UserId, ud.TournamentId });

            
            entity
                .HasQueryFilter(ud => ud.Tournament.IsDeleted == false);


            entity
                .HasOne(ud => ud.User)
                .WithMany(u => u.UserTournaments)
                .HasForeignKey(ud => ud.UserId);


            entity
                .HasOne(ud => ud.Tournament)
                .WithMany(d => d.UserTournaments)
                .HasForeignKey(ud => ud.TournamentId);
        }
    }
}
