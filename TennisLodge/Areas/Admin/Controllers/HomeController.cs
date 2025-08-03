using Microsoft.AspNetCore.Mvc;

namespace TennisLodge.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
