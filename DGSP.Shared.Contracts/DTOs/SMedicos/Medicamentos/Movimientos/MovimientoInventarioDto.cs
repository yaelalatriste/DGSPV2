using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Entradas;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Salidas;
using DGSP.Shared.Contracts.DTOs.Usuarios;

namespace DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Movimientos
{
    public class MovimientoInventarioDto
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

        public UsuarioDto Usuario { get; set; } = new UsuarioDto();
        public LoteDto Lote { get; set; } = new LoteDto ();
        public SalidaMedicamentoDetalleDto SalidaDetalle { get; set; } = new SalidaMedicamentoDetalleDto();
    }
}
