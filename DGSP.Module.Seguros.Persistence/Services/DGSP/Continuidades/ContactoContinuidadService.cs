using DGSP.Module.Seguros.Application.Interfaces.DGSP.Continuidades;
using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using DGSP.Module.Seguros.Domain.DGSP.Continuidades;
using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.MediosContacto;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;

namespace DGSP.Module.Seguros.Persistence.Services.DGSP.Continuidades
{
    public class ContactoContinuidadService : IContactoContinuidadService
    {
        private readonly IContactoContinuidadRepository _contactoContinuidadRepository;

        public ContactoContinuidadService(IContactoContinuidadRepository contactoContinuidadRepository)
        {
            _contactoContinuidadRepository = contactoContinuidadRepository;
        }

        public async Task<List<ContactoContinuidadDto>> GetAllContactoContinuidades()
        {
            var contactos = await _contactoContinuidadRepository.GetAllContactoContinuidades();

            return contactos.Select(x => new ContactoContinuidadDto
            {
                Id = x.Id,
                ContinuidadId = x.ContinuidadId,
                TipoId = x.TipoId,
                Descripcion = x.Descripcion,
                FechaCreacion = x.FechaCreacion,
                FechaActualizacion = x.FechaActualizacion
            }).ToList();
        }

        public async Task<List<ContactoContinuidadDto>> GetContactosByContinuidad(int continuidadId)
        {
            var oficios = await _contactoContinuidadRepository.GetContactosByContinuidad(continuidadId);

            return oficios.Select(x => new ContactoContinuidadDto
            {
                Id = x.Id,
                ContinuidadId = x.ContinuidadId,
                TipoId = x.TipoId,
                Descripcion = x.Descripcion,
                FechaCreacion = x.FechaCreacion,
                FechaActualizacion = x.FechaActualizacion
            }).ToList();
        }

        public async Task<ContactoContinuidadDto> RegistrarContactoContinuidadAsync(RegistrarContactoContinuidadCommand command)
        {
            var contacto = new ContactoContinuidad
            {
                ContinuidadId = command.ContinuidadId,
                TipoId = command.TipoId,
                Descripcion = command.Descripcion,
                FechaCreacion = DateTime.Now,
            };

            await _contactoContinuidadRepository.RegistrarContactoContinuidadAsync(contacto);
            await _contactoContinuidadRepository.SaveChangesAsync();

            return new ContactoContinuidadDto 
            {
                Id = contacto.Id,
                ContinuidadId = contacto.ContinuidadId,
                TipoId = contacto.TipoId,
                Descripcion = contacto.Descripcion,
                FechaCreacion = contacto.FechaCreacion,
            };
        }
       
        public async Task<ContactoContinuidadDto> ActualizarContactoContinuidadAsync(ActualizarContactoContinuidadCommand command)
        {
            var contacto = await _contactoContinuidadRepository.GetContactoById(command.Id);
            contacto.Id = command.Id;
            contacto.ContinuidadId = command.ContinuidadId;
            contacto.TipoId = command.TipoId;
            contacto.Descripcion = command.Descripcion;
            contacto.FechaActualizacion = DateTime.Now;

            await _contactoContinuidadRepository.ActualizarContactoContinuidadAsync(contacto);

            return new ContactoContinuidadDto 
            {
                Id = contacto.Id,
                ContinuidadId = contacto.ContinuidadId,
                TipoId = contacto.TipoId,
                Descripcion = contacto.Descripcion,
                FechaCreacion = contacto.FechaCreacion,
            };
        }
    }
}
