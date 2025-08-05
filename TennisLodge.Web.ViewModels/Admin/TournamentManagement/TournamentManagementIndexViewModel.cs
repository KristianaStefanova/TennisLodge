using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLodge.Web.ViewModels.Admin.TournamentManagement
{
    public class TournamentManagementIndexViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public string Surface { get; set; } = null!;

        public string StartDate { get; set; } = null!;

        public string EndDate { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public string Organizer { get; set; } = null!;

        public bool IsDeleted { get; set; } 

        public string AdminUsername { get; set; } = null!;

        public string RegisteredUsersCount { get; set; } = null!;

        public string AccommodationRequestsCount { get; set; } = null!;
    }
}
