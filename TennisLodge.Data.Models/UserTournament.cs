using System;
using Microsoft.EntityFrameworkCore;

namespace TennisLodge.Data.Models
{
    [Comment("Join table between users and tournaments")]
    public class UserTournament
    {
        [Comment("Foreign key to the user")]
        public string UserId { get; set; } = null!;


        [Comment("Navigation property to the user")]
        public virtual ApplicationUser User { get; set; } = null!;


        [Comment("Foreign key to the tournament")]
        public Guid TournamentId { get; set; }


        [Comment("Navigation property to the tournament")]
        public virtual Tournament Tournament { get; set; } = null!;


        [Comment("Shows if UserTournament entry is deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
