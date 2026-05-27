using System;
using System.Collections.Generic;

namespace HackaTec.Models.Entities;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellidos { get; set; }

    public string Correo { get; set; } = null!;

    public string ContrasenaHash { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string? FotoPerfil { get; set; }

    public virtual ICollection<PublicacionesAgradecimiento> PublicacionesAgradecimiento { get; set; } = new List<PublicacionesAgradecimiento>();

    public virtual ICollection<SalasChat> SalasChat { get; set; } = new List<SalasChat>();
}
