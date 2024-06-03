namespace KillMech.Models
{
    public class partidas_ultimos_seis_meses
    {
        public int PartidaID { get; set; }
        public int JugadorID { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool CampanaCompleta { get; set; }
        public string Mes { get; set; }
        public int NumeroDePartidas { get; set; }
        public int PartidasCompletadas { get; set; }
    }
}
