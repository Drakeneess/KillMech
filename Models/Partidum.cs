using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class Partidum
{
    public int PartidaId { get; set; }

    public int JugadorId { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public bool? CampanaCompleta { get; set; }

    public virtual ICollection<DesempenoCapitulo> DesempenoCapitulos { get; set; } = new List<DesempenoCapitulo>();

    public virtual Jugador Jugador { get; set; } = null!;

    public virtual ICollection<Sesion> Sesions { get; set; } = new List<Sesion>();

    public virtual ICollection<Victorium> Victoria { get; set; } = new List<Victorium>();
}
