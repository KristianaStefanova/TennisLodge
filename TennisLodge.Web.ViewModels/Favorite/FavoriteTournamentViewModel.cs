using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLodge.Web.ViewModels.Favorite
{
    public class FavoriteTournamentViewModel
    {
        public string TournamentId { get; set; } = null!;

        public string TournamentName { get; set; } = null!;

        public string Location { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public string StartDate { get; set; } = null!;

        public string EndDate { get; set; } = null!;

        public string? ImageUrl { get; set; }

    }
}
