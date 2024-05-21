using System;
using System.Collections.Generic;

namespace KillMech.Models;

public partial class Jugador
{
    public int JugadorId { get; set; }

    public string CodigoJugador { get; set; } = null!;

    public string? Nombre { get; set; }

    public TimeOnly? TiempoJugado { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual ICollection<DesafioJugador> DesafioJugadors { get; set; } = new List<DesafioJugador>();

    public virtual ICollection<DesempenoNivel> DesempenoNivels { get; set; } = new List<DesempenoNivel>();

    public virtual ICollection<Partidum> Partida { get; set; } = new List<Partidum>();

    public virtual ICollection<VersionJugador> VersionJugadors { get; set; } = new List<VersionJugador>();

    public virtual ICollection<Victorium> Victoria { get; set; } = new List<Victorium>();
}
