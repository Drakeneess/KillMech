using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class DesempenoJefe
{
    public int DesempenoId { get; set; }

    public int JefeId { get; set; }

    public int DesempenoCapituloId { get; set; }

    public int? ArmaId { get; set; }

    public DateTime? TiempoDerrota { get; set; }

    public virtual Arma? Arma { get; set; }

    public virtual DesempenoCapitulo DesempenoCapitulo { get; set; } = null!;

    public virtual Jefe Jefe { get; set; } = null!;
}
