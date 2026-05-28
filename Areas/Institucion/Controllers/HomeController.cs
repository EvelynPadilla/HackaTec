using HackaTec.Areas.Admin.ViewModels;
using HackaTec.Areas.Institucion.ViewModels;
using HackaTec.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;
using System.Security.Claims;

namespace HackaTec.Areas.Institucion.Controllers
{
    [Authorize]

    [Area("Institucion")]
    public class HomeController : Controller
    {
        private readonly InstitucionService institucionService;

        public HomeController(InstitucionService institucionService)
        {
            this.institucionService = institucionService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

            var model = new IndexViewModel2
            {
                Cct = institucionService.ObtenerCCTPorId(id),
                NombreEscuela = institucionService.ObtenerNombreEscuelaPorId(id),
                Direccion = institucionService.ObtenerDireccionPorId(id),

                Publicaciones = institucionService.ObtenerPublicacionesNecesidad(id).Select(p => new PublicacionesViewModel
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Descripcion = p.Descripcion,
                    Fecha = p.Fecha,
                    IdInstitucion = p.IdInstitucion,
                    Estado = p.Estado
                }).ToList(),

                PublicacionesAgradecimiento = institucionService.ObtenerPublicacionesAgradecimiento(id).Select(a => new PublicacionesAgradecimientoViewModel
                {
                    Id = a.Id,
                    Descripcion = a.Descripcion,
                    Fecha = a.Fecha,
                    IdPublicacionNecesidad = a.IdPublicacionNecesidad,
                    IdUsuarioDonante = a.IdUsuarioDonante,
                    RutaFotografia = a.RutaFotografia
                }).ToList()

            };
            return View(model);
        }


        public IActionResult PublicarNecesidad()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Agradecimientos()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agradecimientos(PublicacionesAgradecimientoViewModel model)
        {
            // Aquí puedes agregar la lógica para guardar el agradecimiento en la base de datos
            // utilizando el servicio correspondiente.
            // Por ejemplo:
            // institucionService.GuardarAgradecimiento(model);
            return RedirectToAction("Index");
        }
    }
}
