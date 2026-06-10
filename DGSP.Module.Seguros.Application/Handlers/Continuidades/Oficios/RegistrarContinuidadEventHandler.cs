using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.OficiosContinuidades;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;
using MediatR;

namespace DGSP.Module.Seguros.Application.Handlers.OficioContinuidades.Oficios
{
    public class RegistrarOficioContinuidadEventHandler : IRequestHandler<RegistrarOficioContinuidadCommand, OficioContinuidadDto>
    {
        private readonly IOficioContinuidadService _OficioContinuidadService;

        public RegistrarOficioContinuidadEventHandler(IOficioContinuidadService OficioContinuidadService)
        {
            _OficioContinuidadService = OficioContinuidadService;
        }
        public async Task<OficioContinuidadDto> Handle(RegistrarOficioContinuidadCommand request, CancellationToken cancellationToken)
        {
            return await _OficioContinuidadService.RegistrarOficioContinuidadService(request);
        }
    }
}
