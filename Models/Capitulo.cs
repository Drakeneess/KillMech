using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class Capitulo
{
    
    public int NivelID { get; set; }

    public string? Nombre { get; set; }

    public int? DificultadId { get; set; }

    public virtual ICollection<DesempenoCapitulo> DesempenoCapitulos { get; set; } = new List<DesempenoCapitulo>();

    public virtual ICollection<DesempenoNivel> DesempenoNivels { get; set; } = new List<DesempenoNivel>();

    public virtual Dificultad? Dificultad { get; set; }
}
