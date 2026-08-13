using DGSP.Module.Seguros.Domain.DGSP.Siniestros.Continuidades;

namespace DGSP.Module.Seguros.Application.Interfaces.DGSP.Siniestros.Continuidades
{
    public interface ILogContinuidadRepository
    {
        Task<List<LogContinuidad>> GetLogsByContinuidad(int continuidadId);
        Task AddLogContinuidadAsync(LogContinuidad entity);
        Task SaveChangesAsync();
    }
}
