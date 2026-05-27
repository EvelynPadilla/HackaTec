using System;
using System.Collections.Generic;

namespace HackaTec.Models.Entities;

public partial class Administrador
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Contrasena { get; set; } = null!;
}
