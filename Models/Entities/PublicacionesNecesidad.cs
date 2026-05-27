using System;
using System.Collections.Generic;

namespace HackaTec.Models.Entities;

public partial class PublicacionesNecesidad
{
    public int Id { get; set; }

    public int IdInstitucion { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateTime? Fecha { get; set; }

    public bool? Estado { get; set; }

    public virtual InstitucionesEducativas IdInstitucionNavigation { get; set; } = null!;

    public virtual ICollection<ImagenesNecesidad> ImagenesNecesidad { get; set; } = new List<ImagenesNecesidad>();

    public virtual ICollection<PublicacionesAgradecimiento> PublicacionesAgradecimiento { get; set; } = new List<PublicacionesAgradecimiento>();

    public virtual ICollection<SalasChat> SalasChat { get; set; } = new List<SalasChat>();
}
