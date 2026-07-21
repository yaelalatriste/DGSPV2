using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.MediosContacto;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;
using MediatR;

namespace DGSP.Module.Seguros.Application.Handlers.Continuidades.Contactos
{
    public class RegistrarContactoContinuidadEventHandler : IRequestHandler<RegistrarContactoContinuidadCommand, ContactoContinuidadDto>
    {
        private readonly IContactoContinuidadService _contactoContinuidad;

        public RegistrarContactoContinuidadEventHandler(IContactoContinuidadService contactoContinuidad)
        {
            _contactoContinuidad = contactoContinuidad;
        }

        public async Task<ContactoContinuidadDto> Handle(RegistrarContactoContinuidadCommand request, CancellationToken cancellationToken)
        {
            return await _contactoContinuidad.RegistrarContactoContinuidadAsync(request);
        }
    }
}
