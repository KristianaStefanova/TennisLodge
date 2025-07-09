using Microsoft.AspNetCore.Mvc;
using TennisLodge.Web.ViewModels.Favorite;

namespace TennisLodge.Web.Controllers
{
    public class FavoriteController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<FavoriteTournamentViewModel> favoriteTournaments 
                = new List<FavoriteTournamentViewModel>();
            return View(favoriteTournaments);
        }
    }
}
