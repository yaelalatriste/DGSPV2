using DGSP.Module.SMedicos.Application.Services.Medicamentos.Entradas;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Entradas;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Entradas;
using MediatR;

namespace DGSP.Module.SMedicos.Application.Handlers.Medicamentos.Entradas
{
    public class RegistrarLoteMedicamentoCommandHandler : IRequestHandler<RegistrarLoteMedicamentoCommand, LoteDto>
    {
        private readonly IInventarioAppService _inventarioAppService;

        public RegistrarLoteMedicamentoCommandHandler(IInventarioAppService inventarioAppService)
        {
            _inventarioAppService = inventarioAppService;
        }

        public async Task<LoteDto> Handle(RegistrarLoteMedicamentoCommand request, CancellationToken cancellationToken)
        {
            return await _inventarioAppService.RegistrarLoteAsync(request);
        }
    }
}
