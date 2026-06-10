using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Correos;
using MediatR;

namespace DGSP.Shared.Contracts.Commands.Seguros.Correos
{
    public class CorreoOrdenPagoCommand : IRequest<CorreoOrdenesPagoDto>
    {
        public int Id { get; set; }
        public bool Enviado { get; set; }
    }
}
