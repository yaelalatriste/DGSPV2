using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.Entregables;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;
using MediatR;

namespace DGSP.Module.Seguros.Application.Handlers.Continuidades.Entregables
{
    public class EliminarEntregableContinuidadEventHandler : IRequestHandler<EliminarEntregableContinuidadCommand, EntregableContinuidadDto>
    {
        private readonly IEntregableContinuidadService _entregableContinuidadService;

        public EliminarEntregableContinuidadEventHandler(IEntregableContinuidadService entregableContinuidadService)
        {
            _entregableContinuidadService = entregableContinuidadService;
        }

        public async Task<EntregableContinuidadDto> Handle(EliminarEntregableContinuidadCommand request, CancellationToken cancellationToken)
        {
            return await _entregableContinuidadService.EliminarEntregableContinuidadAsync(request);
        }
    }
}
