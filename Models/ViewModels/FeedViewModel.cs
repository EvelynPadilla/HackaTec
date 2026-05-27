namespace HackaTec.Models.ViewModels
{
    public class FeedViewModel
    {
        public string? BarraBusqueda { get; set; }
        public List<PublicacionesViewModel> Publicaciones { get; set; } = new List<PublicacionesViewModel>();
    }
    public class PublicacionesViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public DateTime FechaPublicacion { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string? FotoPublicacion { get; set; }
    }
}
