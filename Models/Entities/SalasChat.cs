using System;
using System.Collections.Generic;

namespace HackaTec.Models.Entities;

public partial class SalasChat
{
    public int Id { get; set; }

    public int IdUsuarioDonante { get; set; }

    public int IdInstitucion { get; set; }

    public int? IdPostNecesidad { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual InstitucionesEducativas IdInstitucionNavigation { get; set; } = null!;

    public virtual PublicacionesNecesidad? IdPostNecesidadNavigation { get; set; }

    public virtual Usuario IdUsuarioDonanteNavigation { get; set; } = null!;

    public virtual ICollection<Mensajes> Mensajes { get; set; } = new List<Mensajes>();
}
