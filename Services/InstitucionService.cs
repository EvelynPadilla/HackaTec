using HackaTec.Areas.Admin.ViewModels;
using HackaTec.Areas.Institucion.ViewModels;
using HackaTec.Models.Entities;
using HackaTec.Repositories;
using Microsoft.EntityFrameworkCore;


namespace HackaTec.Services
{
    public class InstitucionService
    {
        private readonly Repository<InstitucionesEducativas> repoEscuelas;
        private readonly Repository<PublicacionesNecesidad> repoPublicaciones;
        private readonly Repository<PublicacionesAgradecimiento> repoAgradecimiento;
        private readonly Repository<PublicacionesNecesidad> repoNecesidad;
        private readonly Repository<Usuario> repoUsuarios;

        public InstitucionService(Repository<InstitucionesEducativas> repoEscuelas, Repository<PublicacionesNecesidad> repoPublicaciones, Repository<PublicacionesAgradecimiento>
            repoAgradecimiento, Repository<PublicacionesNecesidad> repoNecesidad, Repository<Usuario> repoUsuarios)
        {
            this.repoEscuelas = repoEscuelas;
            this.repoPublicaciones = repoPublicaciones;
            this.repoAgradecimiento = repoAgradecimiento;
            this.repoNecesidad = repoNecesidad;
            this.repoUsuarios = repoUsuarios;
        }
        public InstitucionesEducativas? Login(LoginInstitucionesViewModel model)
        {
            bool ingresoCorreo = model.Correo_CCT.Contains("@");

            if (ingresoCorreo)
            {
                return null;
            }
            else
            {
                var institucion = repoEscuelas.GetAll().FirstOrDefault(i =>
                    i.Cct == model.Correo_CCT &&
                    i.Contrasena == model.Contrasena);

                return institucion;
            }
        }

        public InstitucionesEducativas? ObtenerPorId(int id)
        {
            return repoEscuelas.GetAll().FirstOrDefault(i => i.Id == id);
        }

        public List<PublicacionesAgradecimiento> ObtenerPublicacionesAgradecimiento(int id)
        {
            var publicaciones = repoAgradecimiento.GetAll().Where(p => p.IdPublicacionNecesidadNavigation.IdInstitucion == id);
            return publicaciones.ToList();
        }

        public List<PublicacionesNecesidad> ObtenerPublicacionesNecesidad(int id)
        {
            var publicaciones = repoNecesidad.GetAll().Where(p => p.IdInstitucion == id);
            return publicaciones.ToList();
        }

        public string ObtenerCCTPorId(int id)
        {
            var institucion = repoEscuelas.GetAll().FirstOrDefault(i => i.Id == id);
            return institucion != null ? institucion.Cct : string.Empty;
        }

        public string ObtenerNombreEscuelaPorId(int id)
        {
            var institucion = repoEscuelas.GetAll().FirstOrDefault(i => i.Id == id);
            return institucion != null ? institucion.Nombre : string.Empty;
        }

        public string ObtenerDireccionPorId(int id)
        {
            var institucion = repoEscuelas.GetAll().FirstOrDefault(i => i.Id == id);
            return institucion != null ? institucion.Direccion : string.Empty;
        }

        public void GuardarNecesidad(PublicarNecesidadViewModel model)
        {
            var nuevaPublicacion = new PublicacionesNecesidad
            {
                IdInstitucion = model.IdInstitucion,
                Titulo = model.Titulo,
                Descripcion = model.Descripcion,
                Fecha = DateTime.Now, 
                Estado = true 
            };
            repoNecesidad.Insert(nuevaPublicacion);
        }
        //Crear un agradecimiento:

        public void CrearAgradecimiento(PublicacionesAgradecimientoViewModel model)

        {

            var nuevoAgradecimiento = new PublicacionesAgradecimiento

            {

                Descripcion = model.Descripcion,

                IdPublicacionNecesidad = model.IdPublicacionNecesidad,

                IdUsuarioDonante = model.IdUsuarioDonante,

                RutaFotografia = model.RutaFotografia

            };

            repoAgradecimiento.Insert(nuevoAgradecimiento);

        }

        public List<Usuario> ObtenerDonantes()
        {
            return repoUsuarios.GetAll().Where(u => u.Rol=="Donante").ToList();
        }

        public PublicacionesNecesidad? ObtenerPublicacionNecesidadPorId(int id)
        {
            return repoNecesidad.GetAll().FirstOrDefault(p => p.Id == id);
        }

        public void CambiarEstadoNecesidad(int id, bool nuevoEstado)
        {
            var necesidad = repoNecesidad.GetAll().FirstOrDefault(p => p.Id == id);
            if (necesidad != null)
            {
                necesidad.Estado = nuevoEstado;

                if (!nuevoEstado)
                {
                    necesidad.Fecha = DateTime.Now;
                }
                else
                {
                    necesidad.Fecha = null;
                }
                repoNecesidad.Update(necesidad);
            }
        }
    }
}
