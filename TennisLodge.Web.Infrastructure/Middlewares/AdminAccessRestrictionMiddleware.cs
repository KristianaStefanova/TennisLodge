using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Services.Core.Interfaces;
using static TennisLodge.GCommon.ApplicationConstants;

namespace TennisLodge.Web.Infrastructure.Middlewares
{
    public class AdminAccessRestrictionMiddleware
    {
        private const int HttpForbiddenStatusCode = 403;

        private readonly RequestDelegate next;

        public AdminAccessRestrictionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAdminService adminService)
        {
            if(!(context.User.Identity?.IsAuthenticated ?? false))
            {
                bool adminCookieExists = context
                    .Request
                    .Cookies
                    .ContainsKey(AdminAuthCookie);
                if (adminCookieExists)
                {
                    context.Response.Cookies.Delete(AdminAuthCookie);
                }
            }
            
            string requestPath = context.Request.Path.ToString().ToLower();
            if (requestPath.StartsWith("/admin"))
            {
                if (!(context.User.Identity?.IsAuthenticated ?? false))
                {
                    context.Response.StatusCode = HttpForbiddenStatusCode;
                    return;
                }

                bool cookieValueObtained = context
                    .Request
                    .Cookies
                    .TryGetValue(AdminAuthCookie, out string cookieValue);
                if (!cookieValueObtained)
                {
                    string? userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

                    bool isAuthUserAdmin = await adminService
                        .ExistsByUserIdAsync(userId);
                    if (!isAuthUserAdmin)
                    {
                        context.Response.StatusCode = HttpForbiddenStatusCode;
                        return;
                    }

                    await this.AppendAdminAuthCookie(context, userId!);
                }
                else
                {
                    string? userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    if(userId == null)
                    {
                        context.Response.StatusCode = HttpForbiddenStatusCode;
                        return;
                    }

                    string hashedUserId = await this.Sha512OverString(userId);
                    if(hashedUserId.ToLower() != cookieValue.ToLower())
                    {
                        context.Response.StatusCode = HttpForbiddenStatusCode;
                        return;
                    }
                }
            }

            await this.next(context);
        }

        private async Task AppendAdminAuthCookie(HttpContext context, string userId)
        {
            CookieBuilder cookieBuilder = new CookieBuilder()
            {
                Name = AdminAuthCookie,
                SameSite = SameSiteMode.Strict,
                HttpOnly = true,
                SecurePolicy = CookieSecurePolicy.SameAsRequest,
                MaxAge = TimeSpan.FromHours(4),
            };

            CookieOptions cookieOptions = cookieBuilder.Build(context);
            string hashedUserId = await 
                this.Sha512OverString(userId);

            context.Response.Cookies.Append(AdminAuthCookie, hashedUserId, cookieOptions);
                
        }

        private async Task<string> Sha512OverString(string userId)
        {
            using SHA512 sha512Admin = SHA512.Create();

            byte[] sha512HashBytes = await sha512Admin
                .ComputeHashAsync(new MemoryStream(Encoding.UTF8.GetBytes(userId)));

            string hashedString = BitConverter.ToString(sha512HashBytes)
                .Replace("-", "")
                .Trim()
                .ToLower();

            return hashedString;
        }
    }
}