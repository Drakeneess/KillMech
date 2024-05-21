using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class Version
{
    public int VersionId { get; set; }

    public string? NumeroVersion { get; set; }

    public DateTime? FechaLanzamiento { get; set; }

    public virtual ICollection<VersionJugador> VersionJugadors { get; set; } = new List<VersionJugador>();
}
