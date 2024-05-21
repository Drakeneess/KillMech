namespace KillMech.Models
{
    public class desempeñoporjudador
    {
        public int JugadorID { get; set; }
        public string NombreJugador { get; set; }
        public int TiempoTotalJugado { get; set; }
        public int PartidasJugadas { get; set; }
        public int PartidasCompletadas { get; set; }
        public int PartidasAbandonadas { get; set; }
    }
}
