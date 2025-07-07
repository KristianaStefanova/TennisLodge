using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Tournament;
using static TennisLodge.Web.ViewModels.ValidationMessages.Tournament;

namespace TennisLodge.Web.Controllers
{
    public class TournamentController : BaseController
    {
        private readonly ITournamentService tournamentService;
        private readonly ICategoryService categoryService;
        public TournamentController(ITournamentService tournamentService, ICategoryService categoryService)
        {
            this.tournamentService = tournamentService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllTournamentsIndexViewModel> allTournaments = await this.tournamentService
                .GetAllTournamentsAsync();
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
                    ModelState.AddModelError(string.Empty, "Fatal error occurred while adding a tournament");
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
    }
}
