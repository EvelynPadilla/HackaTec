using HackaTec.Areas.Admin.ViewModels;
using HackaTec.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackaTec.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AdminService adminservice;

        public HomeController(AdminService adminService)
        {
            this.adminservice = adminService;
        }

        public IActionResult Index()
        {
            var model = new AdminPanelViewModel
            {
                FormularioInstitucion = new IndexAgregarViewModel(),
                ListaInstituciones = adminservice.ObtenerEscuelas()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult RegistrarInstitucion(AdminPanelViewModel model)
        {
            if (ModelState.IsValid)
            {
                var resultado = adminservice.AgregarEscuela(model.FormularioInstitucion);

                if (resultado == null)
                {
                    TempData["Mensaje"] = "Institución registrada exitosamente";
                    TempData["TipoMensaje"] = "success";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", resultado);
                    TempData["Mensaje"] = resultado;
                    TempData["TipoMensaje"] = "error";
                }
            }

            var viewModel = new AdminPanelViewModel
            {
                FormularioInstitucion = model.FormularioInstitucion,
                ListaInstituciones = adminservice.ObtenerEscuelas()
            };

            return View("Index", viewModel);
        }

        [HttpPost]
        public IActionResult CambiarEstadoInstitucion(int id, string cct)
        {
            var resultado = adminservice.CambiarEstadoEscuela(id);

            if (resultado)
            {
                TempData["Mensaje"] = $"El estado de la institución {cct} ha sido cambiado";
                TempData["TipoMensaje"] = "success";
            }
            else
            {
                TempData["Mensaje"] = "No se pudo cambiar el estado de la institución";
                TempData["TipoMensaje"] = "error";
            }

            return RedirectToAction("Index");
        }
    }
}
