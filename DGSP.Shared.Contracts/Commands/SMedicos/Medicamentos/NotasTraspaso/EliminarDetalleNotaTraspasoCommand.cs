using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;
using MediatR;

namespace DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso
{
    public class EliminarDetalleNotaTraspasoCommand : IRequest<DetalleNotaTraspasoDto>
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public DateTime FechaEliminacion { get; set; }
    }
}
