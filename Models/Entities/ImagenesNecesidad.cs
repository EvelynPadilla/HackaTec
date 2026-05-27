using System;
using System.Collections.Generic;

namespace HackaTec.Models.Entities;

public partial class ImagenesNecesidad
{
    public int Id { get; set; }

    public int IdPublicacion { get; set; }

    public string RutaImagen { get; set; } = null!;

    public virtual PublicacionesNecesidad IdPublicacionNavigation { get; set; } = null!;
}
