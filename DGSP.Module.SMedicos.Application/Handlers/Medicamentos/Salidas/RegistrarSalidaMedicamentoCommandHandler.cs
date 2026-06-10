using DGSP.Module.SMedicos.Application.Services.Medicamentos.Salidas;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Salidas;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Salidas;
using MediatR;

namespace DGSP.Module.SMedicos.Application.Handlers.Medicamentos.Salidas
{
    public class RegistrarSalidaMedicamentoCommandHandler : IRequestHandler<RegistrarSalidaMedicamentoCommand, SalidaMedicamentoDto>
    {
        private readonly ISalidaMedicamentoService _salidaMedicamentoService;

        public RegistrarSalidaMedicamentoCommandHandler(ISalidaMedicamentoService salidaMedicamentoService)
        {
            _salidaMedicamentoService = salidaMedicamentoService;
        }

        public async Task<SalidaMedicamentoDto> Handle(RegistrarSalidaMedicamentoCommand request, CancellationToken cancellationToken)
        {
            return await _salidaMedicamentoService.RegistrarSalidaMedicamentoAsync(request); 
        }
    }
}
