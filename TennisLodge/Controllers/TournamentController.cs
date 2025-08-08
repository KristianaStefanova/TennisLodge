using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Tournament;
using static TennisLodge.Web.ViewModels.ValidationMessages.Tournament;

namespace TennisLodge.Web.Controllers
{
    public class TournamentController : BaseController
    {
        private readonly ITournamentService tournamentService;
        private readonly ICategoryService categoryService;
        private readonly IFavoriteService favoriteService;
        private readonly ITournamentEntryService tournamentEntryService;
        public TournamentController(ITournamentService tournamentService, ICategoryService categoryService,
            IFavoriteService favoriteService, ITournamentEntryService tournamentEntryService)
        {
            this.tournamentService = tournamentService;
            this.categoryService = categoryService;
            this.favoriteService = favoriteService;
            this.tournamentEntryService = tournamentEntryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllTournamentsIndexViewModel> allTournaments = await this.tournamentService
                .GetAllTournamentsAsync();

            if (this.IsUserAuthenticated())
            {
                string playerId = this.GetUserId();

                foreach (AllTournamentsIndexViewModel tournamentIndexVM in allTournaments)
                {
                    tournamentIndexVM.IsAddedToUserFavorites = await this.favoriteService
                        .IsTournamentInFavoritesAsync(tournamentIndexVM.Id, playerId);
                }

               
                IEnumerable<Guid> joinedTournamentIds = await this.tournamentEntryService
                    .GetMyTournamentIdsAsync(playerId);

                foreach (var tournament in allTournaments)
                {
                    tournament.IsUserJoined = joinedTournamentIds.Contains(tournament.Id);
                }
            }

            return View(allTournaments);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string? id)
        {
            try
            {
                string? userId = null;
                if (this.IsUserAuthenticated())
                {
                    userId = this.GetUserId();
                }

                TournamentDetailsViewModel? tournamentDetails = await this.tournamentService
                    .GetTournamentDetailsByIdAsync(id, userId);
                if (tournamentDetails == null)
                {
                    return this.RedirectToAction(nameof(Index));
                }

                return this.View(tournamentDetails);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
