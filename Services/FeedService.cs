using HackaTec.Models.Entities;
using HackaTec.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HackaTec.Services
{
    public class FeedService
    {
        private readonly Repository<PublicacionesAgradecimiento> reposAgradecimeitnos;
        private readonly Repository<PublicacionesNecesidad> reposNecesidad;
        public FeedService(Repository<PublicacionesAgradecimiento> reposAgradecimeitnos, Repository<PublicacionesNecesidad> reposNecesidad)
        {
            this.reposAgradecimeitnos = reposAgradecimeitnos;
            this.reposNecesidad = reposNecesidad;
        }

        public List<PublicacionesAgradecimiento> ObtenerPublicacionesAgradecimiento()
        {
            var publicaciones = reposAgradecimeitnos.GetAll();
            return publicaciones.ToList();
        }
        public string ObtenerNombreInstitucion(int idInstitucion)
        {
            var institucion = reposNecesidad.Query().Include(p => p.IdInstitucionNavigation).FirstOrDefault(p => p.IdInstitucion == idInstitucion);
            return institucion != null ? institucion.IdInstitucionNavigation.Nombre : "Desconocida";
        }

        public List<PublicacionesNecesidad> ObtenerPublicacionesNecesidad()
        {
            var publicaciones = reposNecesidad.GetAll();
            return publicaciones.ToList();
        }

        public List<PublicacionesAgradecimiento> ObtenerPublicacionesAgradecimientoPorPublicacionNecesidad(int idPublicacionNecesidad)
        {
            var publicaciones = reposAgradecimeitnos.GetAll().Where(p => p.IdPublicacionNecesidad == idPublicacionNecesidad);
            return publicaciones.ToList();
        }

        public List<PublicacionesNecesidad> ObtenerPublicacionesNecesidadPorTitulo(string? titulo)
        {
            if (titulo != null)
            {
                var publicaciones = reposNecesidad.GetAll().Where(p => p.Titulo.Contains(titulo));
                return publicaciones.ToList();
            }
            else
            {
                var publicaciones = reposNecesidad.GetAll();
                return publicaciones.ToList();
            }
        }
        public List<PublicacionesAgradecimiento> ObtenerPublicacionesAgradecimientoPorTitulo(string? titulo)
        {
            if (titulo != null)
            {
                var publicaciones = reposAgradecimeitnos.GetAll().Where(p => p.Descripcion.Contains(titulo));
                return publicaciones.ToList();
            }
            else
            {
                var publicaciones = reposAgradecimeitnos.GetAll();
                return publicaciones.ToList();
            }
        }
    }

}
