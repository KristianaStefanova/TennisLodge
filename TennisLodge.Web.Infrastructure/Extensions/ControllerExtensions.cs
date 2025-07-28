using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TennisLodge.Web.Infrastructure.Extensions
{
    public static class ControllerExtensions
    {
        public static string GetUserId(this ControllerBase controller)
        {
            string? userId = controller.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            return userId;
        }
    }
}
