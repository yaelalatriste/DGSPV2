using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Correos;
using MediatR;

namespace DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Correos
{
    public class CorreoCasaCommand : IRequest<CorreoCasaDto>
    {
        public int Id { get; set; }
        public bool Enviado { get; set; }

    }
}
