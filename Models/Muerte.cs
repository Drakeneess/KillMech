using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class Muerte
{
    public int MuerteId { get; set; }

    public int EnemigoId { get; set; }

    public int DesempenoNivelId { get; set; }

    public DateTime? TiempoMuerte { get; set; }

    public float? X { get; set; }

    public float? Y { get; set; }

    public virtual DesempenoCapitulo DesempenoNivel { get; set; } = null!;

    public virtual Enemigo Enemigo { get; set; } = null!;
}
