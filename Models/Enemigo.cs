using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class Enemigo
{
    public int EnemigoId { get; set; }

    public string? Nombre { get; set; }

    public int? CategoriaId { get; set; }

    public int? Mortalidad { get; set; }

    public virtual Categorium? Categoria { get; set; }

    public virtual ICollection<Jefe> Jeves { get; set; } = new List<Jefe>();

    public virtual ICollection<Muerte> Muertes { get; set; } = new List<Muerte>();
}
