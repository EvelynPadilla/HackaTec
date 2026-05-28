using HackaTec.Areas.Admin.ViewModels;
using HackaTec.Areas.Institucion.ViewModels;
using HackaTec.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HackaTec.Areas.Institucion.Controllers
{
    [Area("Institucion")]
    public class AccountController : Controller
    {
        private readonly InstitucionService institucionService;

        public AccountController(InstitucionService institucionService)
        {
            this.institucionService = institucionService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInstitucionesViewModel model)
        {
            var institucion = institucionService.Login(model);

            if (institucion == null)
            {
                ModelState.AddModelError("", "Contraseña o CCT incorrectos");
                return View(model);
            }
            else if (institucion != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,
                    institucion.Id.ToString()),
                    new Claim(ClaimTypes.Name,
                        institucion.Cct),
                    new Claim(ClaimTypes.Role, "Institucion")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Home", new { area = "Institucion" });
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
