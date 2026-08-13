using DGSP.Module.Seguros.Domain.DGSP.Siniestros.Continuidades;

namespace DGSP.Module.Seguros.Application.Interfaces.DGSP.Siniestros.Continuidades
{
    public interface IContactoContinuidadRepository
    {
        Task<List<ContactoContinuidad>> GetAllContactoContinuidades();
        Task<List<ContactoContinuidad>> GetContactosByContinuidad(int continuidadId);
        Task<ContactoContinuidad> GetContactoById(int id);
        Task RegistrarContactoContinuidadAsync(ContactoContinuidad continuidad);
        Task ActualizarContactoContinuidadAsync(ContactoContinuidad continuidad);
        Task SaveChangesAsync();
    }
}
