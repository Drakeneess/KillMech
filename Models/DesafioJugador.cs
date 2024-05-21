using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class DesafioJugador
{
    public int DesafioJugadorId { get; set; }

    public int JugadorId { get; set; }

    public int DesafioId { get; set; }

    public virtual Desafio Desafio { get; set; } = null!;

    public virtual Jugador Jugador { get; set; } = null!;
}
