    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLodge.Web.ViewModels
{
    public class AllTournamentsIndexViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public string Surface { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string StartDate { get; set; } = null!;

        public string EndDate { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public string Organizer { get; set; } = null!;
    }

}
