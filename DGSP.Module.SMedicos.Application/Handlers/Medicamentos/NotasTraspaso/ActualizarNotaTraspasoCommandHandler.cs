using DGSP.Module.SMedicos.Application.Services.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;
using MediatR;

namespace DGSP.Module.SMedicos.Application.Handlers.Medicamentos.NotasTraspaso
{
    public class ActualizarNotaTraspasoCommandHandler : IRequestHandler<ActualizarNotaTraspasoCommand, NotaTraspasoDto>
    {
        private readonly INotaTraspasoQueryService _notaTraspasoQueryService;

        public ActualizarNotaTraspasoCommandHandler(INotaTraspasoQueryService notaTraspasoQueryService)
        {
            _notaTraspasoQueryService = notaTraspasoQueryService;
        }

        public async Task<NotaTraspasoDto> Handle(ActualizarNotaTraspasoCommand request, CancellationToken cancellationToken)
        {
            return await _notaTraspasoQueryService.ActualizarNotaTraspaso(request);
        }
    }
}
