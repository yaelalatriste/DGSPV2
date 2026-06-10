namespace DGSP.Domain.Modulos
{
    public class Modulo
    {
        public int Id { get; set; }
        public string Abreviacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;
        public string Icono { get; set; } = string.Empty;
        public int Orden { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
    }
}
