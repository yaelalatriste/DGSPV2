namespace Clients.WebClient.Models
{
    public class SalidaCapturaInput
    {
        public long? SalidaId { get; set; }
        public int? ConsultaId { get; set; }
        public int ConsultorioId { get; set; }
        public string? ObservacionesGenerales { get; set; }

        public int LoteId { get; set; }
        public int TipoInsumoId { get; set; }
        public int TipoMovimientoId { get; set; }
        public int Cantidad { get; set; }
        public int CantidadEnvase { get; set; }
        public string? FormaFarmaceutica { get; set; }
        public string? TipoEnvase { get; set; }
        public string? ObservacionesDetalle { get; set; }

        public string? MotivoReversion { get; set; }
    }
}
