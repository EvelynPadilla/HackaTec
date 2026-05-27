using Microsoft.AspNetCore.Mvc;

namespace HackaTec.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Opción 1: Redirigir al área Admin
            return RedirectToAction("Feed", "Feed");

            // Opción 2: O mostrar una página de bienvenida pública
            // return View();
        }
    }
}
