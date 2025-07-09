using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Favorite;

namespace TennisLodge.Web.Controllers
{
    public class FavoriteController : BaseController
    {
        private readonly IFavoriteService favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            this.favoriteService = favoriteService; 
        }


        [HttpGet]  
        public async Task<IActionResult> Index()
        {
            try
            {
                string? userId = this.GetUserId();

                if (userId == null)
                {
                    return this.Forbid();
                }
                IEnumerable<FavoriteTournamentViewModel> favoriteTournaments = await this.favoriteService
                    .GetAllFavoriteTournamentsAsync(userId);

                return View(favoriteTournaments);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(string? id)
        {
            try
            {
                string? userId = this.GetUserId();

                if(userId == null)
                {
                    return this.Forbid();
                }

                bool result = await this.favoriteService
                    .AddTournamentToFavoriteAsync(id, userId);

                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Fatal error occurred while adding the tournament to favorites");
                    return this.RedirectToAction(nameof(Index), "Movie");
                }

                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}
