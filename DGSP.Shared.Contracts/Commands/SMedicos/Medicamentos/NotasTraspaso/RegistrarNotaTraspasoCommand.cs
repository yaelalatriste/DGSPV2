using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;
using MediatR;

namespace DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso
{
    public class RegistrarNotaTraspasoCommand : IRequest<NotaTraspasoDto>
    {
        public string UsuarioId { get; set; } = string.Empty;
        public int ConsultorioId { get; set; }
        public int ConsultorioDestinoId { get; set; }
        public string NumeroTraspaso { get; set; } = string.Empty;
        public string Entregable { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }
}
