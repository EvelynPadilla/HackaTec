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

        //Servicios Requeridos
        // Agregar una escuela con sus datos(los datos son los de la clase IndexAgregarViewModel)
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
                Estado = true // Por defecto habilitada al crear
            };
            repoEscuelas.Insert(nuevaEscuela);
            return null; //Exito
        }
        // La escuela no debe de existir previamente, se debe validar que no exista una escuela con el mismo nombre o dirección.
        // Traer los datos de las escuelas para mostrar en las vistas(los daotos estan en la IndexViewModel)
        public List<IndexViewModel> ObtenerEscuelas()
        {
            return repoEscuelas.GetAll().Select(e => new IndexViewModel
            {
                Id = e.Id,
                CCT = e.Cct,
                NombreEscuela = e.Nombre,
                Estado = e.Estado ?? true // Manejo de nulos por seguridad
            }).ToList();
        }
        // Inabiltar o habilitar una escuela, esto se hace cambiando el estado de la escuela, si el estado es false la escuela esta inabilitada y si el estado es true la escuela esta habilitada.
        public bool CambiarEstadoEscuela(int id)
        {
            // Buscamos la escuela (Asumo que puedes usar GetAll().FirstOrDefault o un método GetById)
            var escuela = repoEscuelas.GetAll().FirstOrDefault(e => e.Id == id);

            if (escuela == null)
            {
                return false;
            }

            // Invertimos el estado: si es true pasa a false, y viceversa
            bool estadoActual = escuela.Estado ?? true;
            escuela.Estado = !estadoActual;

            // Actualizamos en la base de datos
            repoEscuelas.Update(escuela);

            return true;
        }
    }
}
