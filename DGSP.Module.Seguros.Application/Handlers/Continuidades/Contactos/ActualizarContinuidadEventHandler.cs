using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.MediosContacto;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;
using MediatR;

namespace DGSP.Module.Seguros.Application.Handlers.Continuidades.Contactos
{
    public class ActualizarContactoContinuidadEventHandler : IRequestHandler<ActualizarContactoContinuidadCommand, ContactoContinuidadDto>
    {
        private readonly IContactoContinuidadService _contactoContinuidad;

        public ActualizarContactoContinuidadEventHandler(IContactoContinuidadService contactoContinuidad)
        {
            _contactoContinuidad = contactoContinuidad;
        }

        public async Task<ContactoContinuidadDto> Handle(ActualizarContactoContinuidadCommand request, CancellationToken cancellationToken)
        {
            return await _contactoContinuidad.ActualizarContactoContinuidadAsync(request);
        }
    }
}
