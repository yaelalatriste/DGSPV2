using DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Logs;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;

namespace DGSP.Module.Seguros.Application.Services.DGSP.Siniestros.Continuidades
{
    public interface ILogContinuidadService
    {
        Task<List<LogContinuidadDto>> GetLogsByContinuidad(int continuidadId);
        Task<LogContinuidadDto> AddLogContinuidadAsync(RegistrarLogContinuidadCommand entity);
    }
}
