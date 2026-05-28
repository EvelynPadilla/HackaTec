using HackaTec.Areas.Institucion.ViewModels;

namespace HackaTec.Areas.Donante.ViewModels
{
    public class PerfilViewModel
    {
        public string Nombre { get; set; } =null!;
        public string Apellidos { get; set; } =null!;
        public string Rol { get; set; } =null!;
        public string? FotoPerfil { get; set; }
        public List<PublicacionesAgradecimientoViewModel> Publicaciones { get; set; } = new List<PublicacionesAgradecimientoViewModel>();
    }
}
