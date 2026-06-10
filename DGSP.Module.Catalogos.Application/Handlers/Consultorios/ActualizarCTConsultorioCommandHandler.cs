using DGSP.Module.Catalogos.Application.Services.SMedicos;
using DGSP.Shared.Contracts.Commands.Catalogos.SMedicos.Consultorios;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using MediatR;

namespace DGSP.Module.Catalogos.Application.Handlers.Consultorios
{
    public class ActualizarCTConsultorioCommandHandler : IRequestHandler<ActualizarCTConsultorioCommand, CTConsultorioDto>
    {
        private readonly ICTConsultorioService _cTConsultorioService;

        public ActualizarCTConsultorioCommandHandler(ICTConsultorioService cTConsultorioService)
        {
            _cTConsultorioService = cTConsultorioService;
        }

        public async Task<CTConsultorioDto> Handle(ActualizarCTConsultorioCommand request, CancellationToken cancellationToken)
        {
            return await _cTConsultorioService.ActualizarConsultorioAsync(request);
        }
    }
}
