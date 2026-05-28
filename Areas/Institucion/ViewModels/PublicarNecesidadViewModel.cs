namespace HackaTec.Areas.Institucion.ViewModels
{
    public class PublicarNecesidadViewModel
    {
        public int Id { get; set; }
        public int IdInstitucion { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime? Fecha { get; set; }
        public bool Estado { get; set; }
    }
}
