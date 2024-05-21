using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class Dificultad
{
    public int DificultadId { get; set; }

    public string? DifultadNombre { get; set; }

    public virtual ICollection<Capitulo> Capitulos { get; set; } = new List<Capitulo>();
}
