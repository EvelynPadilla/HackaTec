using System;
using System.Collections.Generic;

namespace HackaTec.Models.Entities;

public partial class Mensajes
{
    public int Id { get; set; }

    public int IdSala { get; set; }

    public string RemitenteTipo { get; set; } = null!;

    public int IdRemitente { get; set; }

    public string? Contenido { get; set; }

    public string? RutaImagen { get; set; }

    public bool EstadoLeido { get; set; }

    public DateTime? FechaEnvio { get; set; }

    public virtual SalasChat IdSalaNavigation { get; set; } = null!;
}
