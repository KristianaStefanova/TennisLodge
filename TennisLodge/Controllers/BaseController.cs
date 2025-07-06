using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TennisLodge.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected bool IsUserAuthenticated()
        {
            return this.User.Identity?.IsAuthenticated ?? false;
        }
        protected string? GetUserId()
        {
            string? userid = null;

            bool isAuthenticated = this.IsUserAuthenticated();

            if (isAuthenticated)
            {
                userid = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return userid;
        }
        
    }
}
