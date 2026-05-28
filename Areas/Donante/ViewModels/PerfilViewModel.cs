namespace HackaTec.Areas.Donante.ViewModels
{
    public class PerfilViewModel
    {
        public string Nombre { get; set; } =null!;
        public string Apellidos { get; set; } =null!;
        public string Correo { get; set; } =null!;
        public string Telefono { get; set; } = null!;
        public string? FotoPerfil { get; set; }
    }
}
