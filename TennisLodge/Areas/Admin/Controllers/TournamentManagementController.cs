using Microsoft.AspNetCore.Mvc;
using TennisLodge.Services.Core;
using TennisLodge.Services.Core.Admin.Interfaces;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Admin.TournamentManagement;
using TennisLodge.Web.ViewModels.Tournament;
using static TennisLodge.GCommon.ApplicationConstants;

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

                TempData[SuccessMessageKey] = "Tournament created successfully!";

                return this.RedirectToAction(nameof(Manage));
            }
            catch (Exception)
            {
                TempData[ErrorMessageKey] = "An error occurred while creating the tournament. Please contact developer team!";

                return RedirectToAction(nameof(Manage));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            TournamentManagementEditFormModel? editFormModel = await this.tournamentManagementService
                .GetTournamentEditFormModelAsync(id);
            if (editFormModel == null)
            {
                TempData[ErrorMessageKey] = "Selected Tournament does not exist!";

                return this.RedirectToAction(nameof(Manage));
            }

            editFormModel.Categories = await this.categoryService.GetAllCategoriesAsync();

            return this.View(editFormModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TournamentManagementEditFormModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }
            try
            {
                string userId = this.GetUserId()!;
                bool success = await this.tournamentManagementService
                      .EditTournamentAsync(inputModel, userId);
                if (success)
                {
                    TempData[SuccessMessageKey] = "Tournament updated successfully!";
                }
                else
                {
                    TempData[ErrorMessageKey] = "An error occurred while updating the tournament. Please contact developer team!";
                }

                return this.RedirectToAction(nameof(Manage));
            }
            catch (Exception)
            {
                TempData[ErrorMessageKey] = "An error occurred while updating the tournament. Please contact developer team!";

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
