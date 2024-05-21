using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class VersionJugador
{
    public int VersionJugadorId { get; set; }

    public int VersionId { get; set; }

    public int JugadorId { get; set; }

    public virtual Jugador Jugador { get; set; } = null!;

    public virtual Version Version { get; set; } = null!;
}
