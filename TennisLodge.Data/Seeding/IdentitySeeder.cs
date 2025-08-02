using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Data.Models;
using TennisLodge.Data.Seeding.Interfaces;
using static TennisLodge.GCommon.ApplicationConstants;

namespace TennisLodge.Data.Seeding
{
    public class IdentitySeeder : IIdentitySeeder
    {
        private readonly string[] DefaultRoles = { PlayerRoleName, UserRoleName, AdminRoleName };

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserStore<ApplicationUser> userStore;
        private readonly IUserEmailStore<ApplicationUser> emailStore;
        private readonly IConfiguration configuration;
        public IdentitySeeder(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore, IConfiguration configuration)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.userStore = userStore;
            this.configuration = configuration;

            this.emailStore = this.GetEmailStore();
        }
        public async Task SeedIdentityAsync()
        {
            await this.SeedRolesAsync();
            await this.SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            foreach (string defaultRole in DefaultRoles)
            {
                bool roleExists = await this.roleManager
                    .RoleExistsAsync(defaultRole);

                if (!roleExists)
                {
                    IdentityRole newRole = new IdentityRole(defaultRole);

                    IdentityResult result = await this.roleManager
                        .CreateAsync(newRole);
                    if (!result.Succeeded)
                    {
                        throw new Exception($"There was an a exception while seeding the {defaultRole} role!");
                    }
                }
            }
        }

        private async Task SeedUsersAsync()
        {

            string? testUserEmail = this.configuration["UserSeed:TestUser:Email"];
            string? testUserPassword = this.configuration["UserSeed:TestUser:Password"];

            string? playerUserEmail = this.configuration["UserSeed:TestPlayer:Email"];
            string? playerUserPassword = this.configuration["UserSeed:TestPlayer:Password"];

            string? adminUserEmail = this.configuration["UserSeed:TestAdmin:Email"];
            string? adminUserPassword = this.configuration["UserSeed:TestAdmin:Password"];

            if(testUserEmail == null || testUserPassword == null ||
               playerUserEmail == null || playerUserPassword == null ||
               adminUserEmail == null || adminUserPassword == null)
            {
                throw new Exception("The user seeding configuration is not set properly!");
            }

            ApplicationUser testUser = new ApplicationUser();
            ApplicationUser? testUserSeeded = await this.userStore
                .FindByNameAsync(testUserEmail, CancellationToken.None);
            if (testUserSeeded == null)
            {
                await this.userStore.SetUserNameAsync(testUser, testUserEmail, CancellationToken.None);
                await this.emailStore.SetEmailAsync(testUser, testUserEmail, CancellationToken.None);
                IdentityResult result = await this.userManager
                    .CreateAsync(testUser, testUserPassword);
                if(!result.Succeeded)
                {
                    throw new Exception($"There was an a exception while seeding the {testUserEmail} user!");
                }

                result = await this.userManager
                    .AddToRoleAsync(testUser, UserRoleName);

                if (!result.Succeeded)
                {
                    throw new Exception($"There was an a exception while assigning  the {UserRoleName} role to the {testUserEmail} user!");
                }
            }

            ApplicationUser playerUser = new ApplicationUser();
            ApplicationUser? playerUserSeeded = await this.userStore
                .FindByNameAsync(playerUserEmail, CancellationToken.None);
            if (playerUserSeeded == null)
            {
                await this.userStore.SetUserNameAsync(playerUser, playerUserEmail, CancellationToken.None);
                await this.emailStore.SetEmailAsync(playerUser, playerUserEmail, CancellationToken.None);
                IdentityResult result = await this.userManager
                    .CreateAsync(playerUser, playerUserPassword);
                if (!result.Succeeded)
                {
                    throw new Exception($"There was an a exception while seeding the {playerUserEmail} user!");
                }

                result = await userManager
                    .AddToRoleAsync(playerUser, PlayerRoleName);

                if (!result.Succeeded)
                {
                    throw new Exception($"There was an a exception while assigning  the {PlayerRoleName} role to the {playerUserEmail} user!");
                }
            }

            ApplicationUser adminUser = new ApplicationUser();
            ApplicationUser? adminUserSeeded = await this.userStore
                .FindByNameAsync(adminUserEmail, CancellationToken.None);
            if (adminUserSeeded == null)
            {
                await this.userStore.SetUserNameAsync(adminUser, adminUserEmail, CancellationToken.None);
                await this.emailStore.SetEmailAsync(adminUser, adminUserEmail, CancellationToken.None);
                IdentityResult result = await this.userManager
                    .CreateAsync(adminUser, adminUserPassword);
                if (!result.Succeeded)
                {
                    throw new Exception($"There was an a exception while seeding the {adminUserEmail} user!");
                }

                result = await this.userManager
                    .AddToRoleAsync(adminUser, AdminRoleName);
                 
                if (!result.Succeeded)
                {
                    throw new Exception($"There was an a exception while assigning  the {AdminRoleName} role to the {adminUserEmail} user!");
                }
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)userStore;
        }
    }
}
