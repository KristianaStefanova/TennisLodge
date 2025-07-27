using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Linq;
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
        public async Task<IActionResult> Create()
        {
            try
            {
                IEnumerable<CategoryViewModel> categories = await this.categoryService.GetAllCategoriesAsync();

                TournamentFormInputModel viewModel = new TournamentFormInputModel()
                {
                    Categories = categories
                };

                return this.View(viewModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(TournamentFormInputModel inputModel)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    inputModel.Categories = await this.categoryService.GetAllCategoriesAsync();

                    return this.View(inputModel);
                }

                bool addResult = await this.tournamentService
                    .AddTournamentAsync(this.GetUserId()!, inputModel);

                if (addResult == false)
                {
                    ModelState.AddModelError(string.Empty, "Fatal error occurred while adding the tournament");
                    inputModel.Categories = await this.categoryService.GetAllCategoriesAsync();

                    return this.View(inputModel);
                }

                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string? id)
        {
            try
            {
                TournamentDetailsViewModel? tournamentDetails = await this.tournamentService
                    .GetTournamentDetailsByIdAsync(id);
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

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            try
            {
                TournamentFormInputModel? tournamentToEdit = await this.tournamentService
                    .GetEditableTournamentByIdAsync(id);
                if (tournamentToEdit == null)
                {
                    return this.RedirectToAction(nameof(Index));
                }

                tournamentToEdit.Categories = await this.categoryService
                    .GetAllCategoriesAsync();

                return this.View(tournamentToEdit);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TournamentFormInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.Categories = await this.categoryService
                    .GetAllCategoriesAsync();

                return this.View(inputModel);
            }
            try
            {
                bool editResult = await this.tournamentService
                    .EditTournamentAsync(inputModel);
                if (editResult == false)
                {
                    ModelState.AddModelError(string.Empty, "Fatal error occurred while editing the tournament");
                    inputModel.Categories = await this.categoryService.GetAllCategoriesAsync();
                    return this.View(inputModel);
                }
                return this.RedirectToAction(nameof(Details), new { id = inputModel.Id });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            try
            {
                TournamentDetailsViewModel? tournamentDetails = await this.tournamentService
                    .GetTournamentDetailsByIdAsync(id);
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

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction(nameof(Index));
            }

            bool deleteResult = await this.tournamentService.SoftDeleteTournamentAsync(id);

            if (!deleteResult)
            {
                ModelState.AddModelError(string.Empty, "Fatal error occurred while deleting the tournament");
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
