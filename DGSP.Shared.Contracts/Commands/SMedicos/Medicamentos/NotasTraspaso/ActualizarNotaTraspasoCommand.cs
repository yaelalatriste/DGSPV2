using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;
using MediatR;

namespace DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso
{
    public class ActualizarNotaTraspasoCommand : IRequest<NotaTraspasoDto>
    {
        public string UsuarioId { get; set; } = string.Empty;
        public int Id { get; set; }
        public int EstatusId { get; set; }
        public bool Procesar { get; set; }
        public bool Revertir { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public DateTime FechaActualizacion { get; set; }
    }
}
