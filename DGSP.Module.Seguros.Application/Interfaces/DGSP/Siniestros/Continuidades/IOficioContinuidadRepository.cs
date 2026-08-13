using DGSP.Module.Seguros.Domain.DGSP.Siniestros.Continuidades;

namespace DGSP.Module.Seguros.Application.Interfaces.DGSP.Siniestros.Continuidades
{
    public interface IOficioContinuidadRepository
    {
        Task<List<OficioContinuidad>> GetoficiosByContinuidadAsync(int continuidadId);
        Task RegistrarOficioContinuidadAsync(OficioContinuidad continuidad);
        Task SaveChangesAsync();
    }
}
