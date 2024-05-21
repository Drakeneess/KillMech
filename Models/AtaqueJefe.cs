using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class AtaqueJefe
{
    public int AtaqueJefeId { get; set; }

    public int? JefeId { get; set; }

    public string? NombreAtaque { get; set; }

    public int? Mortalidad { get; set; }

    public virtual Jefe? Jefe { get; set; }
}
