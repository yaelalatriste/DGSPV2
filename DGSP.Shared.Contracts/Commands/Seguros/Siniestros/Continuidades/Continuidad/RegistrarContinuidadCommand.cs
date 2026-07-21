using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;
using MediatR;

namespace DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.Continuidad
{
    public class RegistrarContinuidadCommand : IRequest<ContinuidadDto>
    {
        public string UsuarioId { get; set; } = null!;
        public int Expediente { get; set; }
        public DateTime FechaBaja { get; set; }
    }
}
