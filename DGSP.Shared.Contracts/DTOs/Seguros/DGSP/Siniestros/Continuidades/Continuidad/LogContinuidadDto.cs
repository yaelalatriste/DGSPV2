using MediatR;

namespace DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad
{
    public class LogContinuidadDto
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public int ContinuidadId { get; set; }
        public int EstatusId { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }
}
