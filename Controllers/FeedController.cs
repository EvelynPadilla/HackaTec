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
        public IActionResult Feed(string? search)
        {
            FeedViewModel vm = new FeedViewModel();
            vm.BarraBusqueda = search;

            // Cargar necesidades (Publicaciones)
            if (!string.IsNullOrEmpty(search))
            {
                var necesidadesFiltradas = feedService.ObtenerPublicacionesNecesidadPorTitulo(search);
                vm.Publicaciones = necesidadesFiltradas.Select(p => new PublicacionesViewModel
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Descripcion = p.Descripcion,
                    Fecha = p.Fecha,
                    IdInstitucion = p.IdInstitucion,
                    Estado = p.Estado,
                    NombreInstitucion = feedService.ObtenerNombreInstitucion(p.IdInstitucion)
                }).ToList();

                var agradecimientosFiltrados = feedService.ObtenerPublicacionesAgradecimientoPorTitulo(search);
                vm.PublicacionesAgradecimiento = agradecimientosFiltrados.Select(a => new PublicacionesAgradecimientoViewModel
                {
                    Id = a.Id,
                    Descripcion = a.Descripcion,
                    Fecha = a.Fecha,
                    IdPublicacionNecesidad = a.IdPublicacionNecesidad,
                    IdUsuarioDonante = a.IdUsuarioDonante,
                    RutaFotografia = a.RutaFotografia                  
                }).ToList();
            }
            else
            {
                var necesidades = feedService.ObtenerPublicacionesNecesidad();
                vm.Publicaciones = necesidades.Select(p => new PublicacionesViewModel
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Descripcion = p.Descripcion,
                    Fecha = p.Fecha,
                    IdInstitucion = p.IdInstitucion,
                    Estado = p.Estado,
                    NombreInstitucion = feedService.ObtenerNombreInstitucion(p.IdInstitucion)
                }).ToList();

                var agradecimientos = feedService.ObtenerPublicacionesAgradecimiento();
                vm.PublicacionesAgradecimiento = agradecimientos.Select(a => new PublicacionesAgradecimientoViewModel
                {
                    Id = a.Id,
                    Descripcion = a.Descripcion,
                    Fecha = a.Fecha,
                    IdPublicacionNecesidad = a.IdPublicacionNecesidad,
                    IdUsuarioDonante = a.IdUsuarioDonante,
                    RutaFotografia = a.RutaFotografia
                }).ToList();
            }

            return View("FeedView", vm);
        }

        [HttpGet]
        public IActionResult FeedAgradecimientos(string? search)
        {
            // Este método puede ser eliminado si usas la vista unificada.
            // Pero lo dejamos por si lo necesitas para otros fines.
            return RedirectToAction("Feed", new { search });
        }

        [HttpPost]
        public IActionResult Feed(FeedViewModel vm)
        {
            // Redirige a GET para evitar reenvío de formulario
            return RedirectToAction("Feed", new { search = vm.BarraBusqueda });
        }
    }
}
