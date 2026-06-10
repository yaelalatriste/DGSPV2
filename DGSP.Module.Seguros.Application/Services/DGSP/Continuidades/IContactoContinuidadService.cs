using DGSP.Module.Seguros.Domain.DGSP.Continuidades;
using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.MediosContacto;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;

namespace DGSP.Module.Seguros.Application.Services.DGSP.Continuidades
{
    public interface IContactoContinuidadService
    {
        Task<List<ContactoContinuidadDto>> GetAllContactoContinuidades();
        Task<List<ContactoContinuidadDto>> GetContactosByContinuidad(int continuidadId);
        Task<ContactoContinuidadDto> RegistrarContactoContinuidadAsync(RegistrarContactoContinuidadCommand continuidad);
        Task<ContactoContinuidadDto> ActualizarContactoContinuidadAsync(ActualizarContactoContinuidadCommand continuidad);

    }
}
