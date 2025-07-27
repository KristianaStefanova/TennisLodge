using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TennisLodge.Data.Models
{
    [Comment("Extended Identity user with additional profile data")]
    public class ApplicationUser : IdentityUser
    {
        [Comment("User's first name")]
        public string? FirstName { get; set; }


        [Comment("User's last name")]
        public string? LastName { get; set; }


        [Comment("User's city of residence")]
        public string? City { get; set; }


        [Comment("Tournaments the user is registered for")]
        public virtual ICollection<UserTournament> UserTournaments { get; set; }
            = new HashSet<UserTournament>();

        [Comment("Optional player profile with sports-related data")]
        public virtual PlayerProfile? PlayerProfile { get; set; }

        [Comment("Accommodations offered by the user")]
        public virtual ICollection<Accommodation> AccommodationsOffered { get; set; }
            = new HashSet<Accommodation>();

        [Comment("Accommodation requests made by the user")]
        public virtual ICollection<AccommodationRequest> AccommodationRequests { get; set; }
            = new HashSet<AccommodationRequest>();


        [Comment("Tournaments published by the user")]
        public virtual ICollection<Tournament> PublishedTournaments { get; set; }
            = new HashSet<Tournament>();

        [Comment("Collection of tournament entries associated with the user")]
        public virtual ICollection<TournamentEntry> TournamentEntries { get; set; } = new List<TournamentEntry>();

    }
}
