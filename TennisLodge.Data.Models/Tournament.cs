using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TennisLodge.Data.Models
{
    [Comment("Tournament in the system")]
    public class Tournament
    {
        [Comment("Unique identifier for the tournament")]
        public Guid Id { get; set; } = Guid.NewGuid();


        [Comment("Official name of the tournament")]
        public string Name { get; set; } = null!;


        [Comment("Detailed description of the tournament")]
        public string Description { get; set; } = null!;


        [Comment("City or venue where the tournament takes place")]
        public string Location { get; set; } = null!;


        [Comment("Surface type")]
        public string Surface { get; set; } = null!;


        [Comment("Optional URL for the tournament image")]
        public string? ImageUrl { get; set; }


        [Comment("Name of the organizer or organizing body")]
        public string Organizer { get; set; } = null!;


        [Comment("Start date of the tournament")]
        public DateOnly StartDate { get; set; }


        [Comment("End date of the tournament")]
        public DateOnly EndDate { get; set; }


        [Comment("Foreign key to the user who published the tournament")]
        public string? PublisherId { get; set; }

         
        [Comment("Publisher user entity navigation property")]
        public virtual ApplicationUser Publisher { get; set; } = null!;


        [Comment("Foreign key to the tournament category")]
        public int CategoryId { get; set; }


        [Comment("Tournament category navigation property")]
        public virtual Category Category { get; set; } = null!;


        [Comment("Soft delete flag, true if tournament is deleted")]
        public bool IsDeleted { get; set; }

        [Comment("Tournament's admin")]
        public string? AdminId { get; set; }

        [Comment("Foreign key to the admin user")]
        public virtual Admin? Admin { get; set; }


        [Comment("User-Tournament join table navigation property")]
        public virtual ICollection<UserTournament> UserTournaments { get; set; } 
            = new HashSet<UserTournament>();

        public virtual ICollection<AccommodationRequest> AccommodationRequests { get; set; }
            = new HashSet<AccommodationRequest>();
    }
}

