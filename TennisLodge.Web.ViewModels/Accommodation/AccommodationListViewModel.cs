using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLodge.Web.ViewModels.Accommodation
{
    public class AccommodationListViewModel
    {
        public string? TournamentId { get; set; }

        public IEnumerable<AccommodationViewModel> Accommodations { get; set; }
            = new List<AccommodationViewModel>();
    }
}
