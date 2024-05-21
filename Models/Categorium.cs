using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class Categorium
{
    public int CategoriaId { get; set; }

    public string? NombreCategoria { get; set; }

    public virtual ICollection<Enemigo> Enemigos { get; set; } = new List<Enemigo>();
}
