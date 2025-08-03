using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.Services.Core.Interfaces;

namespace TennisLodge.Services.Core
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }
        public async Task<bool> ExistsByUserIdAsync(string? userId)
        {
            bool result = false;

            if (!String.IsNullOrWhiteSpace(userId))
            {
                result = await this.adminRepository
                    .GetAllAttached()
                    .AnyAsync(a => a.UserId.ToLower() == userId.ToLower());
            }
            return result;
        }

        public async Task<bool> ExistsByIdAsync(string? id)
        {
            bool result = false;

            if (!String.IsNullOrWhiteSpace(id))
            {
                result = await this.adminRepository
                    .GetAllAttached()
                    .AnyAsync(a => a.Id.ToLower() == id.ToLower());
            }
            return result;
        }

        public async Task<string?> GetIdByUserIdAsync(string? userId)
        {
            string? adminId = null;

            if (!String.IsNullOrWhiteSpace(userId))
            {
                TennisLodge.Data.Models.Admin? admin = await this.adminRepository
                    .FirstOrDefaultAsync(a => a.UserId.ToLower() == userId.ToLower());

                if (admin != null)
                {
                    adminId = admin.Id;
                }
            }

            return adminId;
        }
    }
}
