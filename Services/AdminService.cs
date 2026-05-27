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
    }
}
