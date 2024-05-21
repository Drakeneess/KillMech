using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class Arma
{
    public int ArmaId { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<DesempenoJefe> DesempenoJeves { get; set; } = new List<DesempenoJefe>();

    public virtual ICollection<UsoArma> UsoArmas { get; set; } = new List<UsoArma>();
}
