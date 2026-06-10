namespace DGSP.Module.Seguros.Domain.DGSP.Continuidades
{
    public class ContactoContinuidad
    {
        public int Id { get; set; }
        public int TipoId { get; set; }
        public int ContinuidadId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
    }
}
