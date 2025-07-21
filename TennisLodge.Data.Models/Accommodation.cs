using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TennisLodge.Data.Models
{
    [Comment("Accommodation offered by a user for hosting players")]
    public class Accommodation
    {
        [Comment("Primary key for the Accommodation entity")]
        public int Id { get; set; }

        
        [Comment("Foreign key to the user offering the accommodation")]
        public string HostUserId { get; set; } = null!;


        [Comment("User offering the accommodation")]
        public virtual ApplicationUser HostUser { get; set; } = null!;
        
        
        [Comment("City where the accommodation is located")]
        public string City { get; set; } = null!;


        [Comment("Address or description of the accommodation")]
        public string Address { get; set; } = null!;


        [Comment("Maximum number of guests that can be hosted")]
        public int MaxGuests { get; set; }


        [Comment("Indicates if the accommodation is currently available")]
        public bool IsAvailable { get; set; } = true;


        [Comment("Optional description or notes about the accommodation")]
        public string? Notes { get; set; }


        [Comment("Date when the offer was created")]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;


        [Comment("Start date of the accommodation availability window")]
        public DateTime? AvailableFrom { get; set; }


        [Comment("End date of the accommodation availability window")]
        public DateTime? AvailableTo { get; set; }

    }
}
