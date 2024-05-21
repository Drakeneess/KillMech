using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class UsoArma
{
    public int UsoArmaId { get; set; }

    public int? DesempenoCapituloId { get; set; }

    public int? ArmaId { get; set; }

    public int? Uso { get; set; }

    public virtual Arma? Arma { get; set; }

    public virtual DesempenoCapitulo? DesempenoCapitulo { get; set; }
}
