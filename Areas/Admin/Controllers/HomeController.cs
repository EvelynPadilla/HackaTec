using HackaTec.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackaTec.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new AdminPanelViewModel
            {
                FormularioInstitucion = new IndexAgregarViewModel(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegistrarInstitucion(IndexAgregarViewModel model)
        {
            if (ModelState.IsValid)
            {
                var nuevaInstitucion = new IndexViewModel
                {
                    NombreEscuela = model.NombreEscuela,
                    CCT = model.CCT,
                    Estado = true 
                };


                TempData["Mensaje"] = "Institución registrada exitosamente";
                return RedirectToAction("Index");
            }

            var viewModel = new AdminPanelViewModel
            {
                FormularioInstitucion = model,
            };

            return View("Index", viewModel);
        }

        [HttpPost]
        public IActionResult DesactivarInstitucion(string cct)
        {
            return RedirectToAction("Index");
        }
    }
}
