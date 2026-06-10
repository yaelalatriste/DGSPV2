using DGSP.Module.Seguros.Domain.DGSP.Continuidades;

namespace DGSP.Module.Seguros.Application.Interfaces.DGSP.Continuidades
{
    public interface IOficioContinuidadRepository
    {
        Task<List<OficioContinuidad>> GetoficiosByContinuidadAsync(int continuidadId);
        Task RegistrarOficioContinuidadAsync(OficioContinuidad continuidad);
        Task SaveChangesAsync();
    }
}
