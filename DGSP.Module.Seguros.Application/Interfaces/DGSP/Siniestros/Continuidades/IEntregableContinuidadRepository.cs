using DGSP.Module.Seguros.Domain.DGSP.Siniestros.Continuidades;

namespace DGSP.Module.Seguros.Application.Interfaces.DGSP.Siniestros.Continuidades
{
    public interface IEntregableContinuidadRepository
    {
        Task<List<EntregableContinuidad>> GetEntregablesByContinuidadAsync(int continuidadId);
        Task<EntregableContinuidad> GetEntregableByIdAsync(int id);
        Task RegistrarEntregableContinuidadAsync(EntregableContinuidad command);
        Task ActualizarEntregableContinuidadAsync(EntregableContinuidad command);
        Task SaveChangesAsync();
    }
}
