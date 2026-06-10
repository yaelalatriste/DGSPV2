namespace DGSP.Domain.SMedicos.Movimientos
{
    public class MovimientoInventario
    {
        public long MovimientoId { get; set; }
        public int? ConsultaId { get; set; }
        public int LoteId { get; set; }
        public int ConsultorioId { get; set; }
        public int TipoInsumoId { get; set; }
        public int Cantidad { get; set; }
        public char Movimiento { get; set; }
        public string FormaFarmaceutica { get; set; } = string.Empty;
        public string Observaciones { get; set; } = "";
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}
