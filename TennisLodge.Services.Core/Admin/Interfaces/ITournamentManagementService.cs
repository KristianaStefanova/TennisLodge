using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Admin.TournamentManagement;

namespace TennisLodge.Services.Core.Admin.Interfaces
{
    public interface ITournamentManagementService : ITournamentService
    {
        Task<IEnumerable<TournamentManagementIndexViewModel>>GetTournamentManagementBoardDataAsync();

        Task<Tuple<bool, bool>> DeleteOrRestoreTournamentAsync(string? id);
    }
}
