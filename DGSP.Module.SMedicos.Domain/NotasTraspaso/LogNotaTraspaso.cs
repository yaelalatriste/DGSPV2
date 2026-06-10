namespace DGSP.Module.SMedicos.Domain.NotasTraspaso
{
    public class LogNotaTraspaso
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public int NotaId { get; set; }
        public int EstatusId { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }
}
