using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLodge.Web.ViewModels.Accommodation
{
    public class AccommodationRequestViewModel
    {
        public int Id { get; set; }
        public string? TournamentName { get; set; }
        public int NumberOfGuests { get; set; }
        public string? Notes { get; set; }
        public bool IsFulfilled { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
