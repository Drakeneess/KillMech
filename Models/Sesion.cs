using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class Sesion
{
    public int SesionId { get; set; }

    public int PartidaId { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public TimeOnly? Duracion { get; set; }

    public int? NivelesCompletados { get; set; }

    public virtual Partidum Partida { get; set; } = null!;
}
