using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TennisLodge.Web.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public abstract class BaseController : Controller
    {
        protected bool IsUserAuthenticated()
        {
            bool retRes = false;
            if(this.User.Identity != null)
            {
                retRes = this.User.Identity?.IsAuthenticated ?? false;
            }

            return retRes;
        }
        protected string? GetUserId()
        {
            string? userid = null;

            bool isAuthenticated = this.IsUserAuthenticated();

            if (isAuthenticated)
            {
                userid = this.User
                    .FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return userid;
        }
    }
}
