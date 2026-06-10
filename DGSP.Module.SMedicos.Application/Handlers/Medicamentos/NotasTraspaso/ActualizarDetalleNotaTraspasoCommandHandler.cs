using DGSP.Module.SMedicos.Application.Services.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;
using MediatR;

namespace DGSP.Module.SMedicos.Application.Handlers.Medicamentos.NotasTraspaso
{
    public class ActualizarDetalleNotaTraspasoCommandHandler : IRequestHandler<ActualizarDetalleNotaTraspasoCommand, DetalleNotaTraspasoDto>
    {
        private readonly IDetalleNotaTraspasoQueryService _detalleNotaTraspasoQueryService;

        public ActualizarDetalleNotaTraspasoCommandHandler(IDetalleNotaTraspasoQueryService detalleNotaTraspasoQueryService)
        {
            _detalleNotaTraspasoQueryService = detalleNotaTraspasoQueryService;
        }

        public async Task<DetalleNotaTraspasoDto> Handle(ActualizarDetalleNotaTraspasoCommand request, CancellationToken cancellationToken)
        {
            return await _detalleNotaTraspasoQueryService.UpdateDetalleNotaTraspaso(request);
        }
    }
}
