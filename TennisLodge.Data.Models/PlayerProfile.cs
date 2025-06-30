using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TennisLodge.Data.Models
{
    [Comment("Player-specific profile with athletic data")]
    public class PlayerProfile
    {
        [Comment("Primary key and foreign key to ApplicationUser")]
        public string UserId { get; set; } = null!;


        [Comment("The user this player profile belongs to")]
        public virtual ApplicationUser User { get; set; } = null!;


        [Comment("Player's date of birth")]
        public DateTime DateOfBirth { get; set; }


        [Comment("Player's nationality")]
        public string Nationality { get; set; } = null!;


        [Comment("Player's ranking, if available")]
        public int? Ranking { get; set; }


        [Comment("Player's preferred playing surface")]
        public string? PreferredSurface { get; set; }


        [Comment("Player's dominant hand (left/right)")]
        public string? DominantHand { get; set; }
    }
}
