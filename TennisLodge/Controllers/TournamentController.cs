using Microsoft.AspNetCore.Mvc;

namespace TennisLodge.Web.Controllers
{
    public class TournamentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
