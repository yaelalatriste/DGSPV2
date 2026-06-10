namespace DGSP.Module.SMedicos.Domain.Inventarios
{
    public class SalidaMedicamento
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public Nullable<int> ConsultaId { get; set; }
        public int ConsultorioId { get; set; }
        public DateTime FechaSalida { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public Nullable<DateTime> FechaCreacion { get; set; }
    }
}
