namespace DGSP.Module.Seguros.Domain.DGSP.Siniestros.Continuidades
{
    public class LogContinuidad
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public int ContinuidadId { get; set; }
        public int EstatusId { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }
}
