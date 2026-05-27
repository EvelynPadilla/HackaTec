using System;
using System.Collections.Generic;

namespace HackaTec.Models.Entities;

public partial class PublicacionesAgradecimiento
{
    public int Id { get; set; }

    public int IdPublicacionNecesidad { get; set; }

    public int IdUsuarioDonante { get; set; }

    public string Descripcion { get; set; } = null!;

    public string? RutaFotografia { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual PublicacionesNecesidad IdPublicacionNecesidadNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioDonanteNavigation { get; set; } = null!;
}
