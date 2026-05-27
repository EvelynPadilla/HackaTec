using HackaTec.Areas.Admin.ViewModels;
using HackaTec.Models.Entities;
using HackaTec.Repositories;

namespace HackaTec.Services
{
    public class AdminService
    {
        private readonly Repository<Administrador> repoUser;

        public AdminService(Repository<Administrador> repoUser) 
        {
            this.repoUser = repoUser;
        }
        public Administrador? Login(LoginAdminVioewModel model)
        {
            var admin = repoUser.GetAll().FirstOrDefault(a => a.Usuario == model.Usuario && a.Contraseña == model.Contraseña);

            return admin;
        }

        //Servicios Requeridos
        // Agregar una escuela con sus datos(los datos son los de la clase IndexAgregarViewModel)
        // La escuela no debe de existir previamente, se debe validar que no exista una escuela con el mismo nombre o dirección.
        // Traer los datos de las escuelas para mostrar en las vistas(los daotos estan en la IndexViewModel)
        // Inabiltar o habilitar una escuela, esto se hace cambiando el estado de la escuela, si el estado es false la escuela esta inabilitada y si el estado es true la escuela esta habilitada.
    }
}
