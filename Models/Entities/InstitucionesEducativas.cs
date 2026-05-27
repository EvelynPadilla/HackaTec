using System;
using System.Collections.Generic;

namespace HackaTec.Models.Entities;

public partial class InstitucionesEducativas
{
    public int Id { get; set; }

    public string Cct { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string PersonaResponsable { get; set; } = null!;

    public string TelefonoEscuela { get; set; } = null!;

    public string TelefonoResponsable { get; set; } = null!;

    public bool? Estado { get; set; }

    public virtual ICollection<PublicacionesNecesidad> PublicacionesNecesidad { get; set; } = new List<PublicacionesNecesidad>();

    public virtual ICollection<SalasChat> SalasChat { get; set; } = new List<SalasChat>();
}
