using DGSP.Module.Seguros.Domain.DGSP.Continuidades;

namespace DGSP.Module.Seguros.Application.Interfaces.Logs
{
    public interface ILogContinuidadRepository
    {
        Task<List<LogContinuidad>> GetLogsByContinuidad(int continuidadId);
        Task AddLogContinuidadAsync(LogContinuidad entity);
        Task SaveChangesAsync();
    }
}
