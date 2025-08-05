using Microsoft.AspNetCore.Mvc;
using TennisLodge.Services.Core;
using TennisLodge.Services.Core.Admin.Interfaces;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Admin.TournamentManagement;
using TennisLodge.Web.ViewModels.Tournament;

namespace TennisLodge.Web.Areas.Admin.Controllers
{
    public class TournamentManagementController : BaseAdminController
    {
        ITournamentManagementService tournamentManagementService;
        ICategoryService categoryService;

        public TournamentManagementController(ITournamentManagementService tournamentManagementService,
            ICategoryService categoryService)
        {
            this.tournamentManagementService = tournamentManagementService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            IEnumerable<TournamentManagementIndexViewModel> allTournaments = await this.tournamentManagementService
                .GetTournamentManagementBoardDataAsync();

            return View(allTournaments);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            IEnumerable<CategoryViewModel> categories = await categoryService
                .GetAllCategoriesAsync();

            TournamentManagementAddFormModel model = new TournamentManagementAddFormModel()
            {
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TournamentManagementAddFormModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                string userId = this.GetUserId()!;

                await this.tournamentManagementService
               .AddTournamentAsync(inputModel, userId);

                return this.RedirectToAction(nameof(Manage));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
