using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HackaTec.Filters
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                // Redirigir al login de Admin
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "Admin" });
            }
            else if (!context.HttpContext.User.IsInRole("Admin"))
            {
                // Si no tiene rol de Admin
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "Admin" });
            }
        }
    }
}