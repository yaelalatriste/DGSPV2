using DGSP.Module.SMedicos.Application.Services.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;
using MediatR;

namespace DGSP.Module.SMedicos.Application.Handlers.Medicamentos.NotasTraspaso
{
    public class RegistrarDetalleNotaTraspasoCommandHandler : IRequestHandler<RegistrarDetalleNotaTraspasoCommand, DetalleNotaTraspasoDto>
    {
        private readonly IDetalleNotaTraspasoQueryService _detalleNotaTraspasoQueryService;

        public RegistrarDetalleNotaTraspasoCommandHandler(IDetalleNotaTraspasoQueryService detalleNotaTraspasoQueryService)
        {
            _detalleNotaTraspasoQueryService = detalleNotaTraspasoQueryService;
        }

        public async Task<DetalleNotaTraspasoDto> Handle(RegistrarDetalleNotaTraspasoCommand request, CancellationToken cancellationToken)
        {
            return await _detalleNotaTraspasoQueryService.AddDetalleNotaTraspaso(request);
        }
    }
}
