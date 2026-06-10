using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Salidas;
using MediatR;

namespace DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Salidas
{
    public class RegistrarSalidaMedicamentoDetalleCommand : IRequest<SalidaMedicamentoDetalleDto>
    {
        public string UsuarioId { get; set; } = string.Empty;
        public int SalidaId { get; set; }
        public int ConsultorioDestinoId { get; set; }
        public int LoteId { get; set; }
        public int TipoInsumoId { get; set; }
        public int TipoMovimientoId { get; set; }
        public int FormaFarmaceuticaId { get; set; }
        public int TipoEnvaseId { get; set; }
        public int Cantidad { get; set; }
        public int CantidadEnvase { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public Nullable<DateTime> FechaCreacion { get; set; }
    }
}
