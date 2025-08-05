using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Web.ViewModels.Admin.TournamentManagement;

namespace TennisLodge.Services.Core.Admin.Interfaces
{
    public interface ITournamentManagementService
    {
        Task<IEnumerable<TournamentManagementIndexViewModel>>GetTournamentManagementBoardDataAsync();

        Task AddTournamentAsync(TournamentManagementAddFormModel? inputModel, string userId);

        Task<TournamentManagementEditFormModel?> GetTournamentEditFormModelAsync(string? id);

        Task<bool> EditTournamentAsync(TournamentManagementEditFormModel? inputModel, string userId);

        Task<Tuple<bool, bool>> DeleteOrRestoreTournamentAsync(string? id);
    }
}
