using DGSP.Module.Seguros.Domain.DGSP.Continuidades;
using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.CEntregables;

namespace DGSP.Module.Seguros.Application.Interfaces.DGSP.Continuidades
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
