using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.CEntregables;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;
using MediatR;

namespace DGSP.Module.Seguros.Application.Handlers.Continuidades.CEntregables
{
    public class ActualizarEntregableContinuidadEventHandler : IRequestHandler<ActualizarEntregableContinuidadCommand, EntregableContinuidadDto>
    {
        private readonly IEntregableContinuidadService _entregableContinuidadService;

        public ActualizarEntregableContinuidadEventHandler(IEntregableContinuidadService entregableContinuidadService)
        {
            _entregableContinuidadService = entregableContinuidadService;
        }

        public async Task<EntregableContinuidadDto> Handle(ActualizarEntregableContinuidadCommand request, CancellationToken cancellationToken)
        {
            return await _entregableContinuidadService.ActualizarEntregableContinuidadAsync(request);
        }
    }
}
