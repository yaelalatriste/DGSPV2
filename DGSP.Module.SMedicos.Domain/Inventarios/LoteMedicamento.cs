namespace DGSP.Module.SMedicos.Domain.Inventarios
{
    public class LoteMedicamento
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public int ConsultorioId { get; set; }
        public int TipoInsumoId { get; set; }
        public int TipoMovimientoId { get; set; }
        public int MedicamentoId { get; set; }
        public int FormaFarmaceuticaId { get; set; }
        public int TipoEnvaseId { get; set; }
        public int UnidadContenidoId { get; set; }
        public string Lote { get; set; } = string.Empty;
        public DateTime FechaCaducidad { get; set; }
        public int Cantidad { get; set; }
        public int CantidadEnvase { get; set; }
        public int CantidadTotal { get; set; }
        public string Concentracion { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public Nullable<DateTime> FechaActualizacion { get; set; }
    }
}
