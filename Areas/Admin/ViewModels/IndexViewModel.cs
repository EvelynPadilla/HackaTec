using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace HackaTec.Areas.Admin.ViewModels
{
    public class IndexViewModel
    {
        public int Id { get; set; }
        public string NombreEscuela { get; set; } = null!;
        public string CCT { get; set; } = null!;
        public bool Estado { get; set; }
    }

    public class IndexAgregarViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        [DataType(DataType.Text)]
        public string NombreEscuela { get; set; } = null!;

        [Required(ErrorMessage = "El CCT es obligatorio.")]
        [MaxLength(10, ErrorMessage = "El CCT no puede exceder los 10 caracteres.")]
        [MinLength(10, ErrorMessage = "El CCT debe tener 10 caracteres.")]
        [DataType(DataType.Text)]
        public string CCT { get; set; } = null!;

        [Required(ErrorMessage = "La direccion es obligatoria.")]
        [StringLength(200, ErrorMessage = "La direccion no puede exceder los 200 caracteres.")]
        [DataType(DataType.Text)]
        public string DireccionEscuela { get; set; } = null!;

        [Required(ErrorMessage = "El responsable es obligatorio.")]
        [StringLength(50, ErrorMessage = "El responsable no puede exceder los 50 caracteres.")]
        [DataType(DataType.Text)]
        public string Responsable { get; set; } = null!;

        [Required(ErrorMessage = "El telefono de la escuela es obligatorio.")]
        [MaxLength(10, ErrorMessage = "El telefono de la escuela no puede exceder los 10 caracteres.")]
        [MinLength(10, ErrorMessage = "El telefono debe tener 10 caracteres.")]
        [DataType(DataType.Text)]
        public string TelefonoEscuela { get; set; } = null!;

        [Required(ErrorMessage = "El telefono del responsable es obligatorio.")]
        [MaxLength(10, ErrorMessage = "El telefono del responsable no puede exceder los 10 caracteres.")]
        [MinLength(10, ErrorMessage = "El telefono del responsable debe tener 10 caracteres.")]
        [DataType(DataType.Text)]
        public string TelefonoResponsable { get; set; } = null!;
    }
}
