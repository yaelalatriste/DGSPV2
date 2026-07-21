using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.Continuidad;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;
using MediatR;

namespace DGSP.Module.Seguros.Application.Handlers.Continuidades.Continuidades
{
    public class RegistrarContinuidadEventHandler : IRequestHandler<RegistrarContinuidadCommand, ContinuidadDto>
    {
        private readonly IContinuidadService _continuidadService;

        public RegistrarContinuidadEventHandler(IContinuidadService continuidadService)
        {
            _continuidadService = continuidadService;
        }
        public async Task<ContinuidadDto> Handle(RegistrarContinuidadCommand request, CancellationToken cancellationToken)
        {
            return await _continuidadService.RegistrarContinuidadAsync(request);
        }
    }
}
