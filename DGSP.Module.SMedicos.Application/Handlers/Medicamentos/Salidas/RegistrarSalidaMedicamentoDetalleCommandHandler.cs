using DGSP.Module.SMedicos.Application.Services.Medicamentos.Salidas;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Salidas;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Salidas;
using MediatR;

namespace DGSP.Module.SMedicos.Application.Handlers.Medicamentos.Salidas
{
    public class RegistrarSalidaMedicamentoDetalleCommandHandler : IRequestHandler<RegistrarSalidaMedicamentoDetalleCommand, SalidaMedicamentoDetalleDto>
    {
        private readonly ISalidaMedicamentoDetalleService _salidaMedicamentoDetalleService;

        public RegistrarSalidaMedicamentoDetalleCommandHandler(ISalidaMedicamentoDetalleService salidaMedicamentoDetalleService)
        {
            _salidaMedicamentoDetalleService = salidaMedicamentoDetalleService;
        }

        public async Task<SalidaMedicamentoDetalleDto> Handle(RegistrarSalidaMedicamentoDetalleCommand request, CancellationToken cancellationToken)
        {
            return await _salidaMedicamentoDetalleService.RegistrarDetalleSalidaMedicamentoAsync(request); 
        }
    }
}
