using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Data.Models;
using TennisLodge.Web.ViewModels.TournamentEntry;

namespace TennisLodge.Services.Core.Interfaces
{
    public interface ITournamentEntryService
    {
        Task<IEnumerable<MyTournamentEntryViewModel>> GetMyTournamentsAsync(string playerId);

        Task<bool> JoinTournamentAsync(string playerId, Guid tournamentId);

        Task<bool> CancelEntryAsync(string playerId, Guid tournamentId);

        Task<IEnumerable<Guid>> GetMyTournamentIdsAsync(string playerId);

    }
}
