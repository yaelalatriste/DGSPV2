using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;
using MediatR;

namespace DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso
{
    public class RegistrarDetalleNotaTraspasoCommand : IRequest<DetalleNotaTraspasoDto>
    {
        public string UsuarioId { get; set; } = string.Empty;
        public int NotaId { get; set; }
        public int LoteId { get; set; }
        public int TipoMovimientoId { get; set; }
        public int Almacen { get; set; }
        public int Cantidad { get; set; }
        public int Restante { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
