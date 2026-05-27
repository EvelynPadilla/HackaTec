using System.ComponentModel.DataAnnotations;

namespace HackaTec.Areas.Institucion.ViewModels
{
    public class LoginInstitucionesViewModel
    {
        string regexEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        [Required(ErrorMessage ="El CCT/Correo es obligatorio")]
        [DataType(DataType.Text)]
        public string Correo_CCT { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, ErrorMessage = "La contraseña no puede exceder los 100 caracteres")]
        [DataType(DataType.Password)]
        public string ContrasenaHash { get; set; }

    }
}
