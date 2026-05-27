using System.ComponentModel.DataAnnotations;

namespace HackaTec.Areas.Donante.ViewModels
{
    public class RegistroDonanteViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres")]
        [DataType(DataType.Text)]
        public string Apellidos { get; set; } = null!;

        [Required(ErrorMessage = "El correo electronico es obligatorio")]
        [StringLength(150, ErrorMessage = "El correo electronico no puede exceder los 150 caracteres")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(255, ErrorMessage = "La contraseña no puede exceder los 255 caracteres")]
        [DataType(DataType.Password)]
        public string ContrasenaHash { get; set; } = null!;

        [Required(ErrorMessage = "El telefono es obligatorio")]
        [StringLength(10, ErrorMessage = "El telefono no puede exceder los 10 caracteres")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; } = null!;
        [Required(ErrorMessage = "El rol es obligatorio")]
        [DataType(DataType.Text)]
        public string Rol { get; set; } = null!;
        [DataType(DataType.ImageUrl)]
        public string? FotoPerfil { get; set; }
    }
}
