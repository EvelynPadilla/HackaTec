using HackaTec.Models.Entities;
using HackaTec.Repositories;

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

        public List<PublicacionesNecesidad> ObtenerPublicacionesNecesidad()
        {
            var publicaciones = reposNecesidad.GetAll();
            return publicaciones.ToList();
        }
        public void MostrarFeed() { 
            var publicacionesAgradecimiento = ObtenerPublicacionesAgradecimiento();
            var publicacionesNecesidad = ObtenerPublicacionesNecesidad();
                var feed = publicacionesAgradecimiento.Cast<object>().Concat(publicacionesNecesidad.Cast<object>()).ToList();
                feed = feed.OrderByDescending(p => 
                {
                    if (p is PublicacionesAgradecimiento agradecimiento)
                        return agradecimiento.Fecha;
                    else if (p is PublicacionesNecesidad necesidad)
                        return necesidad.Fecha;
                    else
                        return DateTime.MinValue;
                }).ToList();
        }
    }
}
