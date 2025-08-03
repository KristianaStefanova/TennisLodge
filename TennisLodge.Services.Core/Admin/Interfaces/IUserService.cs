using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Web.ViewModels.Admin.UserManagement;

namespace TennisLodge.Services.Core.Admin.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserManagementIndexViewModel>> GetUsersManagementBoardDataAsync(string userId);
    }
}
