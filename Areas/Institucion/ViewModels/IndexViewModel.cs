using HackaTec.Models.Entities;

namespace HackaTec.Areas.Institucion.ViewModels
{
    public class IndexViewModel2
    {
        // ==========================================
        // 1. DATOS DEL ENCABEZADO (PERFIL DE LA ESCUELA)
        // ==========================================
        public string NombreEscuela { get; set; } = null!;
        public string Cct { get; set; } = null!;
        public string Direccion { get; set; } = null!; // Para reemplazar: "Zona Escolar: 051..."

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

        public List<Usuario> Donantes { get; set; } = new List<Usuario>();
        public List<PublicacionesNecesidad> Publicaciones { get; set; } = new List<PublicacionesNecesidad>();

    }
}
