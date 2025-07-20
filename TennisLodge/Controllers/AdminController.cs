using Microsoft.AspNetCore.Mvc;

namespace TennisLodge.Web.Controllers
{
    public class AdminController : BaseController
    {
        public IActionResult Index()
        {
            return this.Ok("I am admin!!!");
        }
    }
}
