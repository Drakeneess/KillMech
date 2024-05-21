namespace KillMech.Models
{
    public class DificultadCapitulo
    {
        public string NombreCapitulo { get; set; }
        public decimal HorasPromedioCompletacion { get; set; }
        public decimal DanoRecibidoPromedio { get; set; }
        public decimal DisparosRealizadosPromedio { get; set; }
        public decimal PorcentajeDisparosAcertados { get; set; }
        public decimal TransformacionesRealizadasPromedio { get; set; }
        public decimal PorcentajeCompletados { get; set; }
        public decimal IndiceDificultad { get; set; }
    }
}
