using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
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
        public TournamentController(ITournamentService tournamentService, ICategoryService categoryService,
            IFavoriteService favoriteService)
        {
            this.tournamentService = tournamentService;
            this.categoryService = categoryService;
            this.favoriteService = favoriteService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllTournamentsIndexViewModel> allTournaments = await this.tournamentService
                .GetAllTournamentsAsync();

            if (this.IsUserAuthenticated())
            {

                foreach (AllTournamentsIndexViewModel tournamentIndexVM in allTournaments)
                {
                    tournamentIndexVM.IsAddedToUserFavorites = await this.favoriteService
                        .IsTournamentInFavoritesAsync(tournamentIndexVM.Id, this.GetUserId());
                }
            }

            return View(allTournaments);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var categories = await this.categoryService.GetAllCategoriesAsync();

                var viewModel = new TournamentFormInputModel()
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

        [HttpPost]
        public async Task<IActionResult> Delete(TournamentDetailsViewModel inputModel)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }
                bool deleteResult = await this.tournamentService.SoftDeleteTournamentAsync(inputModel.Id);

                if (deleteResult == false)
                {
                    ModelState.AddModelError(string.Empty, "Fatal error occurred while deleting the tournament");
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
