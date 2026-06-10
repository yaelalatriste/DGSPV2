using DGSP.Module.Catalogos.Application.Services.SMedicos;
using DGSP.Shared.Contracts.Commands.Catalogos.SMedicos.Medicamentos;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using MediatR;

namespace DGSP.Module.Catalogos.Application.Handlers.Medicamentos
{
    public class ActualizarCTMedicamentoCommandHandler : IRequestHandler<ActualizarCTMedicamentoCommand, CTMedicamentoDto>
    {
        private readonly ICTMedicamentoService _cTMedicamentoService;

        public ActualizarCTMedicamentoCommandHandler(ICTMedicamentoService cTMedicamentoService)
        {
            _cTMedicamentoService = cTMedicamentoService;
        }

        public async Task<CTMedicamentoDto> Handle(ActualizarCTMedicamentoCommand request, CancellationToken cancellationToken)
        {
            return await _cTMedicamentoService.ActualizarMedicamentoAsync(request);
        }
    }
}
