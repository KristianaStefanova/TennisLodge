using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLodge.Services.Core.Interfaces
{
    public interface IAdminService
    {
        Task<string?> GetIdByUserIdAsync(string? userId);

        Task<bool> ExistsByIdAsync(string? id);

        Task<bool> ExistsByUserIdAsync(string? userId);
    }
}
