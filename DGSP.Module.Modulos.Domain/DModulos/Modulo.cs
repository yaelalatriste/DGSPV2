namespace DGSP.Module.Modulos.Domain.DModulos
{
    public class Modulo
    {
        public int Id { get; set; }
        public string Abreviacion { get; set; }
        public string Nombre { get; set; }
        public string URL { get; set; }
        public string Icono { get; set; }
        public int Orden { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
    }
}
