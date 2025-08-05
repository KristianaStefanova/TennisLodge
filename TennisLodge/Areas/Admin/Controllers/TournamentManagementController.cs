using Microsoft.AspNetCore.Mvc;
using TennisLodge.Services.Core.Admin.Interfaces;
using TennisLodge.Web.ViewModels.Admin.TournamentManagement;

namespace TennisLodge.Web.Areas.Admin.Controllers
{
    public class TournamentManagementController : BaseAdminController
    {
        ITournamentManagementService tournamentManagementService;

        public TournamentManagementController(ITournamentManagementService tournamentManagementService)
        {
            this.tournamentManagementService = tournamentManagementService;
        }
        public async Task<IActionResult> Manage()
        {
            IEnumerable<TournamentManagementIndexViewModel> allTournaments = await this.tournamentManagementService
                .GetTournamentManagementBoardDataAsync();

            return View(allTournaments);
        }
    }
}
