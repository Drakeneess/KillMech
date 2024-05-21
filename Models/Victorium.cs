using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class Victorium
{
    public int VictoriaId { get; set; }

    public int? PartidaId { get; set; }

    public int? JugadorId { get; set; }

    public int? JefeId { get; set; }

    public TimeOnly? TiempoCompletamiento { get; set; }

    public virtual Jefe? Jefe { get; set; }

    public virtual Jugador? Jugador { get; set; }

    public virtual Partidum? Partida { get; set; }
}
