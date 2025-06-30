using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TennisLodge.Data.Models
{
    [Comment("Request from a user to be hosted during a tournament")]
    public class AccommodationRequest
    {
        [Comment("Primary key of the accommodation request")]
        public int Id { get; set; }

        
        [Comment("Foreign key to the user requesting accommodation")]
        public string GuestUserId { get; set; } = null!;


        [Comment("User who is requesting accommodation")]
        public virtual ApplicationUser GuestUser { get; set; } = null!;

        
        [Comment("Foreign key to the tournament the request is for")]
        public Guid TournamentId { get; set; }


        [Comment("Tournament for which accommodation is requested")]
        public virtual Tournament? Tournament { get; set; } = null!;


        [Comment("Number of people (including the guest) needing accommodation")]
        public int NumberOfGuests { get; set; }


        [Comment("Additional notes or requests from the guest")]
        public string? Notes { get; set; }


        [Comment("Indicates whether the request has been fulfilled or matched")]
        public bool IsFulfilled { get; set; } = false;


        [Comment("Date when the request was created")]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
