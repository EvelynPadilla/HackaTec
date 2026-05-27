namespace HackaTec.Areas.Admin.Models
{
    public class IndexViewModel
    {
        []
        public string NombreEscuela { get; set; }
        public string CCT { get; set; }
        public bool Estado { get; set; }
    }

    public class IndexAgregarViewModel
    {
        public string NombreEscuela { get; set; }
        public string CCT { get; set; }
        public string DireccionEscuela { get; set; }
        public string Responsable { get; set; }
        public string TelefonoEscuela { get; set; }
        public string TelefonoResponsable { get; set; }
    }
}
