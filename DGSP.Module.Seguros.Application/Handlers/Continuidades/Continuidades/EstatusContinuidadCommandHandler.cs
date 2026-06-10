using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.Continuidad;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;
using MediatR;

namespace DGSP.Module.Seguros.Application.Handlers.Continuidades.Continuidades
{
    public class EstatusContinuidadCommandHandler : IRequestHandler<EstatusContinuidadCommand, ContinuidadDto>
    {
        private readonly IContinuidadService _continuidadService;

        public EstatusContinuidadCommandHandler(IContinuidadService continuidadService)
        {
            _continuidadService= continuidadService;
        }

        public async Task<ContinuidadDto> Handle(EstatusContinuidadCommand request, CancellationToken cancellationToken)
        {
            return await _continuidadService.ActualizarEstatusContinuidadAsync(request);
        }
    }
}
