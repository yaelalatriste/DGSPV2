using DGSP.Module.SMedicos.Application.Services.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;
using MediatR;

namespace DGSP.Module.SMedicos.Application.Handlers.Medicamentos.NotasTraspaso
{
    public class EliminarDetalleNotaTraspasoCommandHandler : IRequestHandler<EliminarDetalleNotaTraspasoCommand, DetalleNotaTraspasoDto>
    {
        private readonly IDetalleNotaTraspasoQueryService _detalleNotaTraspasoQueryService;

        public EliminarDetalleNotaTraspasoCommandHandler(IDetalleNotaTraspasoQueryService detalleNotaTraspasoQueryService)
        {
            _detalleNotaTraspasoQueryService = detalleNotaTraspasoQueryService;
        }

        public async Task<DetalleNotaTraspasoDto> Handle(EliminarDetalleNotaTraspasoCommand request, CancellationToken cancellationToken)
        {
            return await _detalleNotaTraspasoQueryService.DeleteDetalleNotaTraspaso(request);
        }
    }
}
