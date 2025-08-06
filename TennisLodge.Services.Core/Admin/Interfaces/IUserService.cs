using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Data.Models;
using TennisLodge.Web.ViewModels.Admin.UserManagement;

namespace TennisLodge.Services.Core.Admin.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserManagementIndexViewModel>> GetUsersManagementBoardDataAsync(string userId);
        Task<bool> AssignRoleAsync(string userId, string role);
        Task<bool> RemoveRoleAsync(string userId, string role);
        Task<bool> DeleteUserAsync(string userId);
        Task<ApplicationUser?> GetUserByIdAsync(string userId);
        Task<bool> HasRelatedDataAsync(string userId);
    }
}
