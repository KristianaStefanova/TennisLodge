using Microsoft.AspNetCore.Mvc;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels;

namespace TennisLodge.Web.Controllers
{
    public class TournamentController : Controller
    {
        private readonly ITournamentService tournamentService;
        public TournamentController(ITournamentService tournamentService)
        {
            this.tournamentService = tournamentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllTournamentsIndexViewModel> allTournaments = await this.tournamentService
                .GetAllTournamentsAsync();
            return View(allTournaments);
        }
    }
}
