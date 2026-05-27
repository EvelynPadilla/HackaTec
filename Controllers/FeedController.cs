using HackaTec.Models.Entities;
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
            var search = vm.BarraBusqueda;
            if (!string.IsNullOrEmpty(search))
            {
                var publicacionesFiltradas = feedService.ObtenerPublicacionesNecesidadPorTitulo(search);
                vm.Publicaciones = publicacionesFiltradas.Select(p => new PublicacionesViewModel
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Descripcion = p.Descripcion,
                    Fecha = p.Fecha,
                    IdInstitucion = p.IdInstitucion,
                    Estado = p.Estado
                }).ToList();
            }
            else
            {
                var publicacionesNecesidad = feedService.ObtenerPublicacionesNecesidad();
                vm.Publicaciones = publicacionesNecesidad.Select(p => new PublicacionesViewModel
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Descripcion = p.Descripcion,
                    Fecha = p.Fecha,
                    IdInstitucion = p.IdInstitucion,
                    Estado = p.Estado
                }).ToList();
            }
            return View("FeedView", vm);
        }
        [HttpGet]
        public IActionResult FeedAgradecimiendos()
        {
            FeedViewModel vm = new FeedViewModel();
            var search = vm.BarraBusqueda;
            if (!string.IsNullOrEmpty(search))
            {
                var publicacionesFiltradas = feedService.ObtenerPublicacionesAgradecimientoPorTitulo(search);
                vm.PublicacionesAgradecimiento = publicacionesFiltradas.Select(p => new PublicacionesAgradecimientoViewModel
                {
                    Id = p.Id,
                    Descripcion = p.Descripcion,
                    Fecha = p.Fecha,
                    IdPublicacionNecesidad = p.IdPublicacionNecesidad,
                    IdUsuarioDonante = p.IdUsuarioDonante,
                    RutaFotografia = p.RutaFotografia
                }).ToList();
            }
            else
            {
                var publicacionesAgradecimiento = feedService.ObtenerPublicacionesAgradecimiento();
                vm.PublicacionesAgradecimiento = publicacionesAgradecimiento.Select(p => new PublicacionesAgradecimientoViewModel
                {
                    Id = p.Id,
                    Descripcion = p.Descripcion,
                    Fecha = p.Fecha,
                    IdPublicacionNecesidad = p.IdPublicacionNecesidad,
                    IdUsuarioDonante = p.IdUsuarioDonante,
                    RutaFotografia = p.RutaFotografia
                }).ToList();
            }
            return View(vm);
        }



        [HttpPost]
        public IActionResult Feed(FeedViewModel vm)
        {
            return View();
        }
    }
}
