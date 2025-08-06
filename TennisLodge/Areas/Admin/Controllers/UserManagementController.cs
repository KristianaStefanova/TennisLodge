using Microsoft.AspNetCore.Mvc;
using TennisLodge.Data.Models;
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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<UserManagementIndexViewModel> allUsers = await this.userService
                .GetUsersManagementBoardDataAsync(this.GetUserId()!);

            return View(allUsers);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmAssignRole(string userId, string role)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
            {
                TempData["ErrorMessage"] = "User and role are required.";
                return RedirectToAction(nameof(Index));
            }

            ApplicationUser? user = await this.userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction(nameof(Index));
            }

            ConfirmAssignRoleViewModel viewModel = new ConfirmAssignRoleViewModel
            {
                UserId = userId,
                UserEmail = user.Email,
                Role = role
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleConfirmed(string userId, string role)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
            {
                TempData["ErrorMessage"] = "User and role are required.";
                return RedirectToAction(nameof(Index));
            }

            bool result = await this.userService.AssignRoleAsync(userId, role);
            
            if (result)
            {
                TempData["SuccessMessage"] = $"Role '{role}' assigned successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Error assigning role. Please verify that the user and role exist.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmRemoveRole(string userId, string role)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
            {
                TempData["ErrorMessage"] = "User and role are required.";
                return RedirectToAction(nameof(Index));
            }

            ApplicationUser? user = await this.userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction(nameof(Index));
            }

            ConfirmRemoveRoleViewModel viewModel = new ConfirmRemoveRoleViewModel
            {
                UserId = userId,
                UserEmail = user.Email,
                Role = role
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRoleConfirmed(string userId, string role)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
            {
                TempData["ErrorMessage"] = "User and role are required.";
                return RedirectToAction(nameof(Index));
            }

            bool result = await this.userService.RemoveRoleAsync(userId, role);
            
            if (result)
            {
                TempData["SuccessMessage"] = $"Role '{role}' removed successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Error removing role. Please verify that the user and role exist.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmDeleteUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = "User ID is required.";
                return RedirectToAction(nameof(Index));
            }

            ApplicationUser? user = await this.userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction(nameof(Index));
            }

            ConfirmDeleteUserViewModel viewModel = new ConfirmDeleteUserViewModel
            {
                UserId = userId,
                UserEmail = user.Email
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserConfirmed(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = "User ID is required.";
                return RedirectToAction(nameof(Index));
            }

            string? currentUserId = this.GetUserId();
            if (userId == currentUserId)
            {
                TempData["ErrorMessage"] = "You cannot delete your own account.";
                return RedirectToAction(nameof(Index));
            }

            ApplicationUser? user = await this.userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction(nameof(Index));
            }

            bool hasRelatedData = await this.userService.HasRelatedDataAsync(userId);
            if (hasRelatedData)
            {
                TempData["ErrorMessage"] = $"Cannot delete user '{user.Email}' because they have associated data (tournament entries, accommodations, etc.). Please remove all associated data first.";
                return RedirectToAction(nameof(Index));
            }

            bool result = await this.userService.DeleteUserAsync(userId);
            
            if (result)
            {
                TempData["SuccessMessage"] = $"User '{user.Email}' deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = $"Error deleting user '{user.Email}'. Please try again or contact support.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string role)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
            {
                TempData["ErrorMessage"] = "User and role are required.";
                return RedirectToAction(nameof(Index));
            }

            bool result = await this.userService.RemoveRoleAsync(userId, role);
            
            if (result)
            {
                TempData["SuccessMessage"] = $"Role '{role}' removed successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Error removing role. Please verify that the user and role exist.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = "User ID is required.";
                return RedirectToAction(nameof(Index));
            }

            bool result = await this.userService.DeleteUserAsync(userId);
            
            if (result)
            {
                TempData["SuccessMessage"] = "User deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Error deleting user. Please verify that the user exists.";
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
