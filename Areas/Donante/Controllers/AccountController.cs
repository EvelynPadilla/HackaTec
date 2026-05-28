using HackaTec.Areas.Donante.ViewModels;
using HackaTec.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HackaTec.Areas.Donante.Controllers
{
    [Area("Donante")]
    public class AccountController : Controller
    {        
        private readonly DonantesService service;

        public AccountController(DonantesService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginDonanteViewModel vm = new();
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDonanteViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var usuario = service.Autenticar(vm);
                if (usuario != null)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Role, "Donante"));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, usuario.Nombre));
                    claims.Add(new Claim("Id", usuario.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Role, "Donante"));
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal, new AuthenticationProperties
                    {
                        IsPersistent = true
                    });
                    return RedirectToAction("Feed", "Feed", new { area = "" });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email o contraseña inválidos.");
                    ViewBag.MostrarError = true;
                    return View(vm);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Email o contraseña inválidos.");
                ViewBag.MostrarError = true;
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult Registrar()
        {
            RegistroDonanteViewModel vm = new RegistroDonanteViewModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Registrar(RegistroDonanteViewModel vm)
        {
            ViewBag.MostrarError = false;
            if (!ModelState.IsValid)
            {
                ViewBag.MostrarError = true;
                return View(vm);
            }

            var (success, message) = service.Registrar(vm);

            if (!success)
            {
                ModelState.AddModelError("", message);
                ViewBag.MostrarError = true;
                return View(vm);
            }

            return RedirectToAction("Login", "Account", new { area = "Donante" });
        }
    }
}
