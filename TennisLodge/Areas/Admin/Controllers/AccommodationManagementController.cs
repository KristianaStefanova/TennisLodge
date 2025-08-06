using Microsoft.AspNetCore.Mvc;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Admin.AccommodationManagement;
using TennisLodge.Web.ViewModels.Accommodation;
using static TennisLodge.GCommon.ApplicationConstants;
using TennisLodge.Services.Core.Admin.Interfaces;

namespace TennisLodge.Web.Areas.Admin.Controllers
{
    public class AccommodationManagementController : BaseAdminController
    {
        private readonly IAccommodationService accommodationService;
        private readonly IAccommodationManagementService accommodationManagementService;
        public AccommodationManagementController(IAccommodationService accommodationService,
            IAccommodationManagementService accommodationManagementService)
        {
            this.accommodationService = accommodationService;
            this.accommodationManagementService = accommodationManagementService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<AccommodationAdminListViewModel> accommodations = await accommodationManagementService
                    .GetAllAccommodationsForAdminAsync();
                return View(accommodations);
            }
            catch (Exception)
            {
                TempData[ErrorMessageKey] = "An error occurred while loading accommodations.";
                return View(new List<AccommodationAdminListViewModel>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                AccommodationCreateInputModel? accommodation = await accommodationService
                    .GetEditableAccommodationByIdAsync(id);
                if (accommodation == null)
                {
                    TempData[ErrorMessageKey] = "Accommodation not found.";
                    return RedirectToAction(nameof(Index));
                }

                return View(accommodation);
            }
            catch (Exception)
            {
                TempData[ErrorMessageKey] = "An error occurred while loading the accommodation.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AccommodationCreateInputModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                bool result = await accommodationService
                    .EditAccommodationAsync(model);
                if (result)
                {
                    TempData[SuccessMessageKey] = "Accommodation updated successfully.";
                }
                else
                {
                    TempData[ErrorMessageKey] = "Failed to update accommodation.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData[ErrorMessageKey] = "An error occurred while updating the accommodation.";
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                bool result = await accommodationService
                    .SoftDeleteAccommodationAsync(id);
                if (result)
                {
                    TempData[SuccessMessageKey] = "Accommodation deleted successfully.";
                }
                else
                {
                    TempData[ErrorMessageKey] = "Failed to delete accommodation.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData[ErrorMessageKey] = "An error occurred while deleting the accommodation.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Restore(string id)
        {
            try
            {
                bool result = await accommodationManagementService
                    .RestoreAccommodationAsync(id);
                if (result)
                {
                    TempData[SuccessMessageKey] = "Accommodation restored successfully.";
                }
                else
                {
                    TempData[ErrorMessageKey] = "Failed to restore accommodation.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData[ErrorMessageKey] = "An error occurred while restoring the accommodation.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Activate(string id)
        {
            try
            {
                bool result = await accommodationManagementService
                    .ActivateAccommodationAsync(id);
                if (result)
                {
                    TempData[SuccessMessageKey] = "Accommodation activated successfully.";
                }
                else
                {
                    TempData[ErrorMessageKey] = "Failed to activate accommodation.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData[ErrorMessageKey] = "An error occurred while activating the accommodation.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Deactivate(string id)
        {
            try
            {
                bool result = await accommodationManagementService
                    .DeactivateAccommodationAsync(id);
                if (result)
                {
                    TempData[SuccessMessageKey] = "Accommodation deactivated successfully.";
                }
                else
                {
                    TempData[ErrorMessageKey] = "Failed to deactivate accommodation.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData[ErrorMessageKey] = "An error occurred while deactivating the accommodation.";

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
