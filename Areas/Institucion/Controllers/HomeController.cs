using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackaTec.Areas.Institucion.Controllers
{
    [Area("Institucion")]
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult PublicarNecesidad()
        {
            return View();
        }
    }
}
