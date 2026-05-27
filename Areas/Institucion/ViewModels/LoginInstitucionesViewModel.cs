using System.ComponentModel.DataAnnotations;

namespace HackaTec.Areas.Institucion.ViewModels
{
    public class LoginInstitucionesViewModel
    {
        [Required(ErrorMessage = "El CCT/Correo es obligatorio")]
        [RegularExpression(@"^([a-zA-Z0-9]{10}|[^@\s]+@[^@\s]+\.[^@\s]+)$", ErrorMessage = "Ingrese un CCT de 10 caracteres o un correo electronico valido")]
        [DataType(DataType.Text)]
        public string Correo_CCT { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, ErrorMessage = "La contraseña no puede exceder los 100 caracteres")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; } = null!;

    }
}
