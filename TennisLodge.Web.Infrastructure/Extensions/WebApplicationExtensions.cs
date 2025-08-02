using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Data.Seeding.Interfaces;
using TennisLodge.Web.Infrastructure.Middlewares;

namespace TennisLodge.Web.Infrastructure.Extensions
{
    public static class WebApplicationExtensions
    {
        public static IApplicationBuilder UseAdminAccessRestriction(this IApplicationBuilder app)
        {
            app.UseMiddleware<AdminAccessRestrictionMiddleware>();

            return app;
        }

        public static IApplicationBuilder SeedDefaultIdentity(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            IServiceProvider serviceProvider = scope.ServiceProvider;

            IIdentitySeeder identitySeeder = serviceProvider
                .GetRequiredService<IIdentitySeeder>();

            identitySeeder
                .SeedIdentityAsync()
                .GetAwaiter()
                .GetResult();

            return app;
        }
    }
}
