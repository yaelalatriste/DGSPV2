namespace DGSP.Module.SMedicos.Domain.NotasTraspaso
{
    public class NotaTraspaso
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public int ConsultorioId { get; set; }
        public int ConsultorioDestinoId { get; set; }
        public int EstatusId { get; set; }
        public string NumeroTraspaso { get; set; } = string.Empty;
        public string Entregable { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public Nullable<DateTime> FechaActualizacion { get; set; }
    }
}
