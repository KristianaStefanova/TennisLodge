using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLodge.Web.ViewModels.TournamentEntry
{
    public class MyTournamentEntryViewModel
    {
        public int EntryId { get; set; }

        public string TournamentName { get; set; } = null!;

        public DateOnly TournamentStartDate { get; set; }

        public string TournamentLocation { get; set; } = null!;

        public DateTime RegisteredOn { get; set; }
    }
}
