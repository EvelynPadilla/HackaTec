namespace HackaTec.Models.ViewModels
{
    public class FeedViewModel
    {
        public string? BarraBusqueda { get; set; }
        public List<PublicacionesViewModel> Publicaciones { get; set; } = new List<PublicacionesViewModel>();
        public List<PublicacionesAgradecimientoViewModel> PublicacionesAgradecimiento { get; set; } = new List<PublicacionesAgradecimientoViewModel>();
    }
    public class PublicacionesViewModel
    {
        public int Id { get; set; }

        public int IdInstitucion { get; set; }

        public string Titulo { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public DateTime? Fecha { get; set; }

        public bool? Estado { get; set; }

    }
    public class PublicacionesAgradecimientoViewModel
    {
        public int Id { get; set; }

        public int IdPublicacionNecesidad { get; set; } //id de la publicación de necesidad a la que se agradece

        public int IdUsuarioDonante { get; set; }

        public string Descripcion { get; set; } = null!;

        public string? RutaFotografia { get; set; }
        public DateTime? Fecha { get; set; }

    }
}
