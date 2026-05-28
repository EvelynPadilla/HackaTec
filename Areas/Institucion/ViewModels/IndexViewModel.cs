namespace HackaTec.Areas.Institucion.ViewModels
{
    public class IndexViewModel
    {
        // ==========================================
        // 1. DATOS DEL ENCABEZADO (PERFIL DE LA ESCUELA)
        // ==========================================
        public string NombreEscuela { get; set; } = null!;
        public string Cct { get; set; } = null!;
        public string Direccion { get; set; } = null!; // Para reemplazar: "Zona Escolar: 051..."

        // ==========================================
        // 2. LISTAS PARA LAS PESTAÑAS
        // ==========================================
        public List<NecesidadItemViewModel> Necesidades { get; set; } = new List<NecesidadItemViewModel>();
        public List<AgradecimientoItemViewModel> Agradecimientos { get; set; } = new List<AgradecimientoItemViewModel>();
    }

    // ==========================================
    // CLASES AUXILIARES PARA LAS LISTAS
    // (Puedes dejarlas en este mismo archivo para mantener el orden)
    // ==========================================

    public class NecesidadItemViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime FechaPublicacion { get; set; }
        public bool EstadoActivo { get; set; } // true = Activo, false = Cubierto
    }

    public class AgradecimientoItemViewModel
    {
        public int Id { get; set; }
        public string NombreDonante { get; set; } = null!;
        public string TituloDonacion { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime FechaAgradecimiento { get; set; }
    }
}
