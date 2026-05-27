using HackaTec.Areas.Admin.ViewModels;
using HackaTec.Areas.Institucion.ViewModels;
using HackaTec.Models.Entities;
using HackaTec.Repositories;


namespace HackaTec.Services
{
    public class InstitucionService
    {
        private readonly Repository<InstitucionesEducativas> repoEscuelas;
        private readonly Repository<PublicacionesNecesidad> repoPublicaciones;
        public InstitucionService(Repository<InstitucionesEducativas> repoEscuelas, Repository<PublicacionesNecesidad> repoPublicaciones)
        {
            this.repoEscuelas = repoEscuelas;
            this.repoPublicaciones = repoPublicaciones;
        }
        public InstitucionesEducativas? Login(LoginInstitucionesViewModel model)
        {bool ingresoCorreo = model.Correo_CCT.Contains("@");

            if (ingresoCorreo)
            {
                // ATENCIÓN: La tabla instituciones_educativas NO tiene un campo 'correo'.
                // Por lo tanto, si escriben un correo, no hay forma de encontrar la escuela
                // usando repoEscuelas. Retornamos null porque las credenciales son inválidas aquí.
                return null;
            }
            else
            {
                // Como no tiene '@', asumimos que es el CCT validado por tu ViewModel.
                // Buscamos que coincida tanto el CCT como la contraseña.
                var institucion = repoEscuelas.GetAll().FirstOrDefault(i =>
                    i.Cct == model.Correo_CCT &&
                    i.Contrasena == model.Contrasena);

                return institucion;
            }
        }
    }
}
