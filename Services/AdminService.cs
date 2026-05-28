using HackaTec.Areas.Admin.ViewModels;
using HackaTec.Models.Entities;
using HackaTec.Repositories;

namespace HackaTec.Services
{
    public class AdminService
    {
        private readonly Repository<Administrador> repoUser;
        private readonly Repository<InstitucionesEducativas> repoEscuelas;

        public AdminService(Repository<Administrador> repoUser, Repository<InstitucionesEducativas> repoEscuelas) 
        {
            this.repoUser = repoUser;
            this.repoEscuelas = repoEscuelas;
        }
        public Administrador? Login(LoginAdminVioewModel model)
        {
            var admin = repoUser.GetAll().FirstOrDefault(a => a.Nombre == model.Usuario && a.Contrasena == model.Contraseña);

            return admin;
        }

    
        public string? AgregarEscuela(IndexAgregarViewModel modelo)
        {
            bool existe = repoEscuelas.GetAll().Any(e =>
                e.Nombre.ToLower() == modelo.NombreEscuela.ToLower() ||
                e.Direccion.ToLower() == modelo.DireccionEscuela.ToLower());

            if (existe)
            {
                return "La escuela ya se encuentra registrada con ese mismo nombre o dirección.";
            }
            var nuevaEscuela = new InstitucionesEducativas{ 
                Cct=modelo.CCT,
                Nombre = modelo.NombreEscuela,
                Direccion = modelo.DireccionEscuela,
                PersonaResponsable = modelo.Responsable,
                TelefonoEscuela = modelo.TelefonoEscuela,
                TelefonoResponsable = modelo.TelefonoResponsable,
                Contrasena = modelo.Contraseña,
                Estado = true 
            };
            repoEscuelas.Insert(nuevaEscuela);
            return null; 
        }
 
        public List<IndexViewModel> ObtenerEscuelas()
        {
            return repoEscuelas.GetAll().Select(e => new IndexViewModel
            {
                Id = e.Id,
                CCT = e.Cct,
                NombreEscuela = e.Nombre,
                Estado = e.Estado ?? true 
            }).ToList();
        }
        public bool CambiarEstadoEscuela(int id)
        {
            var escuela = repoEscuelas.GetAll().FirstOrDefault(e => e.Id == id);

            if (escuela == null)
            {
                return false;
            }

            bool estadoActual = escuela.Estado ?? true;
            escuela.Estado = !estadoActual;

            repoEscuelas.Update(escuela);

            return true;
        }
    }
}
