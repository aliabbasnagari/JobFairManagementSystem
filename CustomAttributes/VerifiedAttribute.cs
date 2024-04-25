using System.Security.Claims;
using JobFairManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace JobFairManagementSystem.CustomAttributes;

public class VerifiedAttribute() : TypeFilterAttribute(typeof(VerifiedFilter))
{
    public class VerifiedFilter(UserManager<ApplicationUser> userManager) : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user.Identity is { IsAuthenticated: true })
            {
                var applicationUser = userManager.GetUserAsync(user).Result;

                if (applicationUser is not { IsVerified: true })
                {
                    context.Result = new RedirectResult("~/Account/VerificationPending"); // Redirect to a verification pending page
                }
            }
            else
            {
                context.Result = new RedirectResult("~/Account/Login"); // Redirect to login page if not authenticated
            }
        }
    }
}
