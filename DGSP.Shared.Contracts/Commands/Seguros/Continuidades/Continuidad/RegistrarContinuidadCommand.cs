using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;
using MediatR;

namespace DGSP.Shared.Contracts.Commands.Seguros.Continuidades.Continuidad
{
    public class RegistrarContinuidadCommand : IRequest<ContinuidadDto>
    {
        public string UsuarioId { get; set; } = null!;
        public int Expediente { get; set; }
        public DateTime FechaBaja { get; set; }
    }
}
