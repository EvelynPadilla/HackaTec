using HackaTec.Areas.Donante.ViewModels;
using HackaTec.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackaTec.Areas.Donante.Controllers
{
    [Area("Donante")]
    public class HomeController : Controller
    {
        private readonly DonantesService service;
        
        public HomeController(DonantesService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()    
        {
            PerfilViewModel perfilViewModel = new PerfilViewModel();
            perfilViewModel = service.ObtenerPerfil(int.Parse(User.FindFirst("Id")?.Value ?? "0"));
            return View(perfilViewModel);
        }
    }
}
