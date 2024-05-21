using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class Desafio
{
    public int DesafioId { get; set; }

    public string? NombreDesafio { get; set; }

    public virtual ICollection<DesafioJugador> DesafioJugadors { get; set; } = new List<DesafioJugador>();
}
