using DGSP.Module.Seguros.Domain.DGSP.Continuidades;

namespace DGSP.Module.Seguros.Application.Interfaces.DGSP.Continuidades
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
