using DGSP.Module.Catalogos.Application.Services.SMedicos;
using DGSP.Shared.Contracts.Commands.Catalogos.SMedicos.Medicamentos;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using MediatR;

namespace DGSP.Module.Catalogos.Application.Handlers.Medicamentos
{
    public class RegistrarCTMedicamentoCommandHandler : IRequestHandler<RegistrarCTMedicamentoCommand, CTMedicamentoDto>
    {
        private readonly ICTMedicamentoService _cTMedicamentoService;

        public RegistrarCTMedicamentoCommandHandler(ICTMedicamentoService cTMedicamentoService)
        {
            _cTMedicamentoService = cTMedicamentoService;
        }

        public async Task<CTMedicamentoDto> Handle(RegistrarCTMedicamentoCommand request, CancellationToken cancellationToken)
        {
            return await _cTMedicamentoService.RegistrarMedicamentoAsync(request);
        }
    }
}
