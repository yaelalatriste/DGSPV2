using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;
using MediatR;

namespace DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.Continuidad
{
    public class ActualizarContinuidadCommand : IRequest<ContinuidadDto>
    {
        public int Id { get; set; }
        public int EstatusId { get; set; }
        public string UsuarioId { get; set; } = null!;
        public DateTime FechaBaja { get; set; }
        public DateTime FechaEnvioSP { get; set; }
        public DateTime FechaLimitePago { get; set; }
        public decimal Importe { get; set; }
        public bool Pagado { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
