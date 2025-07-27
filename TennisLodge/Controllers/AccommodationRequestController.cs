using Microsoft.AspNetCore.Mvc;
using TennisLodge.Services.Core;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Accommodation;

namespace TennisLodge.Web.Controllers
{
    public class AccommodationRequestController : BaseController
    {
        private readonly IAccommodationRequestService accommodationRequestService;
        private readonly ITournamentService tournamentService;

        public AccommodationRequestController(IAccommodationRequestService accommodationRequestService,
            ITournamentService tournamentService)
        {
            this.accommodationRequestService = accommodationRequestService;
            this.tournamentService = tournamentService;
        }


        [HttpGet]
        public async Task<IActionResult> CreateRequest()
        {
            AccommodationRequestInputModel model = new AccommodationRequestInputModel
            {
                Tournaments = await tournamentService.GetAllAsSelectList(),
            };

            return this.View("CreateRequest", model);
        }


        [HttpPost]
        public async Task<IActionResult> SendRequest(AccommodationRequestInputModel inputModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    inputModel.Tournaments = await tournamentService
                        .GetAllAsSelectList();
                    return this.View(inputModel);
                }

                string? guestUserId = this.GetUserId();
                if (guestUserId == null)
                {
                    return this.Forbid();
                }

                await this.accommodationRequestService
                    .CreateAccommodationRequestAsync(guestUserId, inputModel);

                return this.RedirectToAction("Index", "Accommodation");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                inputModel.Tournaments = await tournamentService.GetAllAsSelectList();

                ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again.");
                return this.View(inputModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> MyRequests()
        {
            string? userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            try
            {
                IEnumerable<AccommodationRequestViewModel> requests = await this.accommodationRequestService
                    .GetRequestsByUserIdAsync(userId);
                return View(requests);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                {
                    return this.RedirectToAction();
                }
            }
        }
    }
}
