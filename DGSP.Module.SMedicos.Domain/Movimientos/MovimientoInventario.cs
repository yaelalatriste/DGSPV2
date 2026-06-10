namespace DGSP.Module.SMedicos.Domain.Movimientos
{
    public class MovimientoInventario
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public int LoteId { get; set; }
        public string TipoMovimiento { get; set; } = string.Empty;
        public string Origen { get; set; } = string.Empty;
        public Nullable<int> ReferenciaId { get; set; }
        public Nullable<int> SalidaDetalleId { get; set; }
        public int Cantidad { get; set; }
        public int CantidadEnvase { get; set; }
        public int CantidadTotal { get; set; }
        public int ExistenciaAnterior { get; set; }
        public int ExistenciaPosterior { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string Observaciones { get; set; } = string.Empty;
    }
}
