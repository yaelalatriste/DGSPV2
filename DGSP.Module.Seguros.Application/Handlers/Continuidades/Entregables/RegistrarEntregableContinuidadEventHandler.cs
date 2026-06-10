using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.CEntregables;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;
using MediatR;

namespace DGSP.Module.Seguros.Application.Handlers.Continuidades.CEntregables
{
    public class RegistrarEntregableContinuidadEventHandler : IRequestHandler<RegistrarEntregableContinuidadCommand, EntregableContinuidadDto>
    {
        private readonly IEntregableContinuidadService _entregableContinuidadService;

        public RegistrarEntregableContinuidadEventHandler(IEntregableContinuidadService entregableContinuidadService)
        {
            _entregableContinuidadService = entregableContinuidadService;
        }

        public async Task<EntregableContinuidadDto> Handle(RegistrarEntregableContinuidadCommand request, CancellationToken cancellationToken)
        {
            return await _entregableContinuidadService.RegistrarEntregableContinuidadAsync(request);
        }
    }
}
