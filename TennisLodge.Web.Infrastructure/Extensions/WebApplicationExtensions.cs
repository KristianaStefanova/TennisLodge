using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
    }
}
