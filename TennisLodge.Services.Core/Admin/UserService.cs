using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisLodge.Data.Models;
using TennisLodge.Services.Core.Admin.Interfaces;
using TennisLodge.Web.ViewModels.Admin.UserManagement;
using TennisLodge.Data;

namespace TennisLodge.Services.Core.Admin
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly TennisLodgeDbContext dbContext;

        public UserService(UserManager<ApplicationUser> userManager, TennisLodgeDbContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<UserManagementIndexViewModel>> GetUsersManagementBoardDataAsync(string userId)
        {
            IEnumerable<UserManagementIndexViewModel> users = await this.userManager
                .Users
                .Where(u => u.Id.ToLower() != userId.ToLower())
                .Select(u => new UserManagementIndexViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    Roles = userManager.GetRolesAsync(u)
                        .GetAwaiter()
                        .GetResult()
                })
                .ToArrayAsync();

            return users;
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
        {
            return await this.userManager.FindByIdAsync(userId);
        }

        public async Task<bool> AssignRoleAsync(string userId, string role)
        {
            try
            {
                ApplicationUser? user = await this.userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return false;
                }

                IdentityResult result = await this.userManager.AddToRoleAsync(user, role);
                return result.Succeeded;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveRoleAsync(string userId, string role)
        {
            try
            {
                ApplicationUser? user = await this.userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return false;
                }

                IdentityResult result = await this.userManager.RemoveFromRoleAsync(user, role);
                return result.Succeeded;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            try
            {
                ApplicationUser? user = await this.userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return false;
                }

                IList<string> userRoles = await this.userManager.GetRolesAsync(user);
                
                if (userRoles.Any())
                {
                    await this.userManager.RemoveFromRolesAsync(user, userRoles);
                }

                try
                {
                    await dbContext.Database.ExecuteSqlRawAsync(@"
                        DELETE FROM TournamentEntries WHERE PlayerId = {0};
                        DELETE FROM UserTournaments WHERE UserId = {0};
                        DELETE FROM AccommodationRequests WHERE GuestUserId = {0};
                        DELETE FROM Accommodations WHERE HostUserId = {0};
                        DELETE FROM PlayerProfiles WHERE UserId = {0};
                    ", userId);
                }
                catch
                {
                    // Continue with user deletion even if related data deletion fails
                }

                IdentityResult result = await this.userManager
                    .DeleteAsync(user);

                return result.Succeeded;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> HasRelatedDataAsync(string userId)
        {
            try
            {
                bool hasPlayerProfile = await dbContext.PlayerProfiles
                    .AnyAsync(p => p.UserId == userId);

                if (hasPlayerProfile) return true;

                bool hasTournamentEntries = await dbContext.TournamentEntries
                    .AnyAsync(te => te.PlayerId == userId);

                if (hasTournamentEntries) return true;

                bool hasUserTournaments = await dbContext.UserTournaments
                    .AnyAsync(ut => ut.UserId == userId);

                if (hasUserTournaments) return true;

                bool hasAccommodationRequests = await dbContext.AccommodationRequests
                    .AnyAsync(ar => ar.GuestUserId == userId);

                if (hasAccommodationRequests)
                {
                    return true;
                }

                bool hasAccommodations = await dbContext.Accommodations
                    .AnyAsync(a => a.HostUserId == userId);

                return hasAccommodations;
            }
            catch
            {
                return true;
            }
        }
    }
}
