using Microsoft.AspNetCore.Mvc;
using TennisLodge.Services.Core;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Accommodation;

namespace TennisLodge.Web.Controllers
{
    public class AccommodationController : BaseController
    {
        private readonly IAccommodationService accommodationService;

        public AccommodationController(IAccommodationService accommodationService)
        {
            this.accommodationService = accommodationService;
        }

        public async Task<IActionResult> Index()
        {
            var accommodations = await accommodationService
                .GetAvailableAccommodationsAsync();

            return View(accommodations);
        }

        public IActionResult Add()
        {
            AccommodationCreateInputModel model = accommodationService
                .GetCreateModel();

            return View(model);
        }

    }
}
