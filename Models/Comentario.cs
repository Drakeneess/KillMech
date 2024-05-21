using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class Comentario
{
    public int ComentarioId { get; set; }

    public int JugadorId { get; set; }

    public string? Comentario1 { get; set; }

    public virtual Jugador Jugador { get; set; } = null!;
}
