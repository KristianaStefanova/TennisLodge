using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Security.Claims;
using TennisLodge.Services.Core;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Accommodation;
using TennisLodge.Web.ViewModels.Tournament;

namespace TennisLodge.Web.Controllers
{
    public class AccommodationController : BaseController
    {
        private readonly IAccommodationService accommodationService;

        public AccommodationController(IAccommodationService accommodationService)
        {
            this.accommodationService = accommodationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AccommodationViewModel> allAccommodations = await this.accommodationService
                .GetAllAccommodationsAsync();

            if (this.IsUserAuthenticated())
            {

                foreach (AccommodationViewModel accommodationViewModel in allAccommodations)
                {
                    accommodationViewModel.IsOwner = await this.accommodationService
                        .IsAccommodationAddedFromUserAsync(accommodationViewModel.Id.ToString(), this.GetUserId());
                }
            }

            return this.View(allAccommodations);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AccommodationCreateInputModel model = this.accommodationService
                .GetCreateModel();

            return this.View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Add(AccommodationCreateInputModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.View(model);
                }

                bool isAdded = await this.accommodationService
                    .AddAccommodationAsync(this.GetUserId()!, model);

                if (!isAdded)
                {
                    ModelState.AddModelError(string.Empty, "Failed to save the accommodation. Please try again.");
                    return this.View(model);
                }

                return RedirectToAction(nameof(Index));
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
                AccommodationCreateInputModel? editableMovie = await this.accommodationService
                    .GetEditableAccommodationByIdAsync(id);
                if (editableMovie == null)
                {
                    return this.RedirectToAction(nameof(Index));
                }

                return this.View(editableMovie);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AccommodationCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                bool editSuccess = await this.accommodationService
                    .EditAccommodationAsync(inputModel);
                if (!editSuccess)
                {
                    return this.RedirectToAction(nameof(Index));
                }

                return this.RedirectToAction(nameof(Index), new { id = inputModel.Id });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            try
            {
                AccommodationViewModel? accommodationDetails = await this.accommodationService
                    .GetAccomodationDeleteDetailsByIdAsync(id);
                if (accommodationDetails == null)
                {
                    return this.RedirectToAction(nameof(Index));
                }
                return this.View(accommodationDetails);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
