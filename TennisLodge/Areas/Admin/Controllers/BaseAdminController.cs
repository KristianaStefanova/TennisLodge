using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static TennisLodge.GCommon.ApplicationConstants;

namespace TennisLodge.Web.Areas.Admin.Controllers
{
    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]
    public abstract class BaseAdminController : Controller
    {
        private bool IsUserAuthenticated()
        {
            bool retRes = false;
            if (this.User.Identity != null)
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
