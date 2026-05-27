using System;
using System.Collections.Generic;

namespace HackaTec.Models.Entities;

public partial class Administrador
{
    public int Id { get; set; }

    public string Correo { get; set; } = null!;

    public string Contraseña { get; set; } = null!;
}
