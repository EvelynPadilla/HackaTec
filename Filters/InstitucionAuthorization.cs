using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HackaTec.Filters
{
    public class InstitucionAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "Institucion" });
            }
            else if (!context.HttpContext.User.HasClaim(c => c.Type == "InstitucionId"))
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "Institucion" });
            }
        }
    }
}