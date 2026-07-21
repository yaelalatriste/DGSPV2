using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;
using MediatR;

namespace DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.Continuidad
{
    public class EstatusContinuidadCommand : IRequest<ContinuidadDto>
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public int EstatusId { get; set; }
        public bool Corregir { get; set; }
        public bool Pagada { get; set; }
        public string Observaciones { get; set; } = string.Empty;
    }
}
