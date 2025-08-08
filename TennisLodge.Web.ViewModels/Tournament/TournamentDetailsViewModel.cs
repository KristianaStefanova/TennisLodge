using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLodge.Web.ViewModels.Tournament
{
    public class TournamentDetailsViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string Description { get; set; } = null!;

        public string Location { get; set; } = null!;

        public string Surface { get; set; } = null!;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string CategoryName { get; set; } = null!;

        public string Organizer { get; set; } = null!;

        public bool IsInFavorites { get; set; }
    }
}
