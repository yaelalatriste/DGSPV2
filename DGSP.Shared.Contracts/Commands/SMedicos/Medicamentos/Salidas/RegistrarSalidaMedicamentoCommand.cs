using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Salidas;
using MediatR;

namespace DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Salidas
{
    public class RegistrarSalidaMedicamentoCommand : IRequest<SalidaMedicamentoDto>
    {
        public int ConsultaId { get; set; }
        public int ConsultorioId { get;set; }
        public string UsuarioId { get; set; } = string.Empty;
        public DateTime FechaSalida { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }
}
