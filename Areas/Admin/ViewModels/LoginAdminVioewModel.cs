using System.ComponentModel.DataAnnotations;

namespace HackaTec.Areas.Admin.ViewModels
{
    public class LoginAdminVioewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        [DataType(DataType.Text)]
        public string Usuario { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres.")]
        [DataType(DataType.Password)]        
        public string Contraseña { get; set; } = null!;
    }
}
