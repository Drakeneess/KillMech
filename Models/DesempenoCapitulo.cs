using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class DesempenoCapitulo
{
    public int DesempenoCapituloId { get; set; }

    public int PartidaId { get; set; }

    public int NivelId { get; set; }

    public bool? Completo { get; set; }

    public TimeOnly? TiempoCompletacion { get; set; }

    public float? DanoTotalHecho { get; set; }

    public float? DanoTotalRecibido { get; set; }

    public int? EvasionesRealizadas { get; set; }

    public int? DisparosRealizados { get; set; }

    public int? DisparosAcertados { get; set; }

    public int? TransformacionesRealizadas { get; set; }

    public float? DanoJetTotal { get; set; }

    public float? DanoMechaTotal { get; set; }

    public virtual ICollection<DesempenoJefe> DesempenoJeves { get; set; } = new List<DesempenoJefe>();

    public virtual ICollection<Muerte> Muertes { get; set; } = new List<Muerte>();

    public virtual Capitulo Nivel { get; set; } = null!;

    public virtual Partidum Partida { get; set; } = null!;

    public virtual ICollection<UsoArma> UsoArmas { get; set; } = new List<UsoArma>();
}
