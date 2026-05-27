using HackaTec.Models.ViewModels;
using HackaTec.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackaTec.Controllers
{
    public class FeedController : Controller
    {
        private readonly FeedService feedService;
        public FeedController(FeedService feedService)
        {
            this.feedService = feedService;
        }
        [HttpGet]
        public IActionResult Feed()
        {
            FeedViewModel vm = new FeedViewModel();
            return View("FeedView", vm);   
        }

        [HttpPost]
        public IActionResult Feed(FeedViewModel vm)
        {
            var publicacionesAgradecimiento = feedService.ObtenerPublicacionesAgradecimiento();
            var publicacionesNecesidad = feedService.ObtenerPublicacionesNecesidad();
            vm.Publicaciones = publicacionesAgradecimiento.Select(p => new PublicacionesViewModel
            {
                Id = p.Id,
                Descripcion = p.Descripcion,
                Tipo = "Agradecimiento",
                FechaPublicacion = p.Fecha ?? DateTime.Now,
            }).ToList();
            vm.Publicaciones.AddRange(publicacionesNecesidad.Select(p => new PublicacionesViewModel
            {
                Id = p.Id,
                Titulo = p.Titulo,
                Descripcion = p.Descripcion,
                Tipo = "Necesidad",
                FechaPublicacion = p.Fecha ?? DateTime.Now,
            }));
            vm.Publicaciones = vm.Publicaciones.OrderByDescending(p => p.FechaPublicacion).ToList();

            return View("FeedView", vm); 
        }
    }
}
