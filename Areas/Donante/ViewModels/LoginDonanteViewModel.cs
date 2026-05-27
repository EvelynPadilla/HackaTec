using System.ComponentModel.DataAnnotations;

namespace HackaTec.Areas.Donante.ViewModels
{
    public class LoginDonanteViewModel
    {
        [Required(ErrorMessage = "El correo electronico es obligatorio")]
        [StringLength(150, ErrorMessage = "El correo electronico no puede exceder los 150 caracteres")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(255, ErrorMessage = "La contraseña no puede exceder los 255 caracteres")]
        [DataType(DataType.Password)]
        public string ContrasenaHash { get; set; } = null!;
    }
}
