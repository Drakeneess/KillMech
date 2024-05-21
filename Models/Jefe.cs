using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class Jefe
{
    public int JefeId { get; set; }

    public int EnemigoId { get; set; }

    public virtual ICollection<AtaqueJefe> AtaqueJeves { get; set; } = new List<AtaqueJefe>();

    public virtual ICollection<DesempenoJefe> DesempenoJeves { get; set; } = new List<DesempenoJefe>();

    public virtual Enemigo Enemigo { get; set; } = null!;

    public virtual ICollection<Victorium> Victoria { get; set; } = new List<Victorium>();
}
