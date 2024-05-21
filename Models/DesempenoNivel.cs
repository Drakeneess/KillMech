using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class DesempenoNivel
{
    public int DesempenoId { get; set; }

    public int? NivelId { get; set; }

    public int? JugadorId { get; set; }

    public TimeOnly? TiempoCompletamiento { get; set; }

    public bool? Completa { get; set; }

    public int? SuperpoderId { get; set; }

    public virtual Jugador? Jugador { get; set; }

    public virtual Capitulo? Nivel { get; set; }
}
