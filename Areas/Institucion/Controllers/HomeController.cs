using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackaTec.Areas.Institucion.Controllers
{
    [Authorize]

    [Area("Institucion")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PublicarNecesidad()
        {
            return View();
        }
    }
}
