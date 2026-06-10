using DGSP.Module.SMedicos.Application.Services.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;
using MediatR;

namespace DGSP.Module.SMedicos.Application.Handlers.Medicamentos.NotasTraspaso
{
    public class RegistrarNotaTraspasoCommandHandler : IRequestHandler<RegistrarNotaTraspasoCommand, NotaTraspasoDto>
    {
        private readonly INotaTraspasoQueryService _notaTraspasoQueryService;

        public RegistrarNotaTraspasoCommandHandler(INotaTraspasoQueryService notaTraspasoQueryService)
        {
            _notaTraspasoQueryService = notaTraspasoQueryService;
        }

        public async Task<NotaTraspasoDto> Handle(RegistrarNotaTraspasoCommand request, CancellationToken cancellationToken)
        {
            return await _notaTraspasoQueryService.AddNotaTraspaso(request);
        }
    }
}
