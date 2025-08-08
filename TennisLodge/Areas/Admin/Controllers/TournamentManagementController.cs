using Microsoft.AspNetCore.Mvc;
using TennisLodge.Services.Core.Admin.Interfaces;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Admin.TournamentManagement;
using TennisLodge.Web.ViewModels.Tournament;
using static TennisLodge.GCommon.ApplicationConstants;

namespace TennisLodge.Web.Areas.Admin.Controllers
{
    public class TournamentManagementController : BaseAdminController
    {
        private readonly ITournamentManagementService tournamentManagementService;
        private readonly ICategoryService categoryService;
        private readonly ILogger<TournamentManagementController> logger;

        public TournamentManagementController(ITournamentManagementService tournamentManagementService,
            ICategoryService categoryService, ILogger<TournamentManagementController> logger)
        {
            this.tournamentManagementService = tournamentManagementService;
            this.categoryService = categoryService;
            this.logger = logger;
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

            TournamentFormInputModel model = new TournamentFormInputModel()
            {
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TournamentFormInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                string userId = this.GetUserId()!;

                await this.tournamentManagementService
                      .AddTournamentAsync(userId, inputModel);

                TempData[SuccessMessageKey] = "Tournament created successfully!";

                return this.RedirectToAction(nameof(Manage));
            }
            catch (Exception e)
            {
                this.logger.LogCritical(e.Message);

                TempData[ErrorMessageKey] = "Fatal error occurred while creating the tournament. Please contact developer team!";

                return RedirectToAction(nameof(Manage));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            try
            {
                TournamentFormInputModel? editFormModel = await this.tournamentManagementService
                .GetEditableTournamentByIdAsync(id);
                if (editFormModel == null)
                {
                    TempData[ErrorMessageKey] = "Selected Tournament does not exist!";

                    return this.NotFound();
                }

                editFormModel.Categories = await this.categoryService
                    .GetAllCategoriesAsync();

                return this.View(editFormModel);
            }
            catch (Exception e)
            {
                this.logger.LogCritical(e.Message);

                TempData[ErrorMessageKey] = "Fatal error occurred while updating the tournament. Please contact developer team!";

                return RedirectToAction(nameof(Manage));
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TournamentFormInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }
            try
            {
                string userId = this.GetUserId()!;
                bool success = await this.tournamentManagementService
                      .EditTournamentAsync(inputModel);
                if (success)
                {
                    TempData[SuccessMessageKey] = "Tournament updated successfully!";
                }
                else
                {
                    TempData[ErrorMessageKey] = "Selected Tournament does not exist!";
                }

                return this.RedirectToAction(nameof(Manage));
            }
            catch (Exception e)
            {
                this.logger.LogCritical(e.Message);

                TempData[ErrorMessageKey] = "Fatal error occurred while updating the tournament. Please contact developer team!";

                return RedirectToAction(nameof(Manage));
            }
        }

        [HttpGet]
        public async Task<IActionResult> ToggleDelete(string? id)
        {
            Tuple<bool, bool> opResult = await this.tournamentManagementService
                .DeleteOrRestoreTournamentAsync(id);
            bool success = opResult.Item1;  
            bool isRestored = opResult.Item2;   

            if (!success)
            {
                TempData[ErrorMessageKey] = "Tournament's could not be found and updated!";
            }
            else
            {
                string operation = isRestored ? "restored" : "deleted";

                TempData[SuccessMessageKey] = $"Tournament {operation} successfully!";
            }

            return RedirectToAction(nameof(Manage));
        }
    }
}
