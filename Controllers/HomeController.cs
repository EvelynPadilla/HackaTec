using Microsoft.AspNetCore.Mvc;

namespace HackaTec.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Feed", "Feed");
        }
    }
}
