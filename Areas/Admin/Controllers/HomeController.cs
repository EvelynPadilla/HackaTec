using Microsoft.AspNetCore.Mvc;

namespace HackaTec.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
