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

        [HttpGet]
        public IActionResult PublicarNecesidad()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
            PublicarNecesidadViewModel model = new PublicarNecesidadViewModel
            {
                IdInstitucion = id
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult PublicarNecesidad(PublicarNecesidadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var institucion = institucionService.ObtenerPorId(model.IdInstitucion);
                if (institucion == null)
                {
                    ModelState.AddModelError("", "Institución no encontrada");
                    return View(model);
                }

                model.Fecha = DateTime.Now;
                model.Estado = true; 

                institucionService.GuardarNecesidad(model);


                TempData["SuccessMessage"] = "Necesidad publicada exitosamente";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al publicar la necesidad: {ex.Message}");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult PublicarAgradecimiento()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

            var model = new PublicacionesAgradecimientoViewModel
            {
                Donantes = institucionService.ObtenerDonantes(),
                Publicaciones = institucionService.ObtenerPublicacionesNecesidad(id)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult PublicarAgradecimiento(PublicacionesAgradecimientoViewModel model)
        {
            // Aquí puedes agregar la lógica para guardar el agradecimiento en la base de datos
            // utilizando el servicio correspondiente.
            // Por ejemplo:
            // institucionService.GuardarAgradecimiento(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var publicacionNecesidad = institucionService.ObtenerPublicacionesNecesidad(model.IdPublicacionNecesidad).FirstOrDefault();

                int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

                var institucion = institucionService.ObtenerPorId(id);
                if (institucion == null)
                {
                    ModelState.AddModelError("", "Institución no encontrada");
                    return View(model);
                }

                model.Fecha = DateTime.Now;

                institucionService.CrearAgradecimiento(model);


                TempData["SuccessMessage"] = "Agradecimiento publicado exitosamente";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al publicar el agradecimiento: {ex.Message}");
                model.Donantes = institucionService.ObtenerDonantes();
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CambiarEstadoNecesidad(int id, bool nuevoEstado)
        {
            try
            {
                int idInstitucion = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

                var necesidad = institucionService.ObtenerPublicacionNecesidadPorId(id);

                if (necesidad == null)
                {
                    TempData["ErrorMessage"] = "No se encontró la necesidad especificada.";
                    return RedirectToAction("Index");
                }

                if (necesidad.IdInstitucion != idInstitucion)
                {
                    TempData["ErrorMessage"] = "No tienes permiso para modificar esta necesidad.";
                    return RedirectToAction("Index");
                }

                institucionService.CambiarEstadoNecesidad(id, nuevoEstado);

                string mensaje = nuevoEstado ?
                    "La necesidad ha sido reactivada exitosamente." :
                    "La necesidad ha sido marcada como cubierta exitosamente.";

                TempData["SuccessMessage"] = mensaje;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al cambiar el estado de la necesidad: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}
