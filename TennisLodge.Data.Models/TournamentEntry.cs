using System;
using Microsoft.EntityFrameworkCore; // Necesario para usar [Comment]

namespace TennisLodge.Data.Models
{
    [Comment("Represents a player's participation in a tournament")]
    public class TournamentEntry
    {
        [Comment("Primary key of the tournament entry")]
        public int Id { get; set; }

        [Comment("Foreign key to the player (ApplicationUser)")]
        public string PlayerId { get; set; } = null!;

        [Comment("Navigation property to the player")]
        public virtual ApplicationUser Player { get; set; } = null!;

        [Comment("Foreign key to the tournament")]
        public Guid? TournamentId { get; set; }

        [Comment("Navigation property to the tournament")]
        public virtual Tournament Tournament { get; set; } = null!;

        [Comment("Date when the player registered for the tournament")]
        public DateTime RegisteredOn { get; set; } = DateTime.UtcNow;

        [Comment("Indicates whether the entry has been soft-deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
