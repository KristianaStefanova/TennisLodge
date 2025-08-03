using Microsoft.AspNetCore.Mvc;
using TennisLodge.Services.Core.Admin.Interfaces;
using TennisLodge.Web.ViewModels.Admin.UserManagement;

namespace TennisLodge.Web.Areas.Admin.Controllers
{
    public class UserManagementController : BaseAdminController
    {
        private readonly IUserService userService;

        public UserManagementController(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<UserManagementIndexViewModel> allUsers = await this.userService
                .GetUsersManagementBoardDataAsync(this.GetUserId()!);

            return View(allUsers);
        }
    }
}
