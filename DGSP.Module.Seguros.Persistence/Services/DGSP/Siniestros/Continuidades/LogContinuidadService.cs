using DGSP.Module.Seguros.Application.Interfaces.DGSP.Siniestros.Continuidades;
using DGSP.Module.Seguros.Application.Services.DGSP.Siniestros.Continuidades;
using DGSP.Module.Seguros.Domain.DGSP.Siniestros.Continuidades;
using DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Logs;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;

namespace DGSP.Module.Seguros.Persistence.Services.DGSP.Siniestros.Continuidades
{
    public class LogContinuidadService : ILogContinuidadService
    {
        private readonly ILogContinuidadRepository _logContinuidadRepository;

        public LogContinuidadService(ILogContinuidadRepository logContinuidadRepository)
        {
            _logContinuidadRepository = logContinuidadRepository;
        }

        public async Task<LogContinuidadDto> AddLogContinuidadAsync(RegistrarLogContinuidadCommand command)
        {
            var log = new LogContinuidad { 
                UsuarioId = command.UsuarioId,
                ContinuidadId = command.ContinuidadId,
                EstatusId = command.EstatusId,
                Observaciones = command.Observaciones,
                FechaCreacion = DateTime.Now
            };

            await _logContinuidadRepository.AddLogContinuidadAsync(log);
            await _logContinuidadRepository.SaveChangesAsync();

            return new LogContinuidadDto
            {
                Id = log.Id,
                UsuarioId = log.UsuarioId,
                ContinuidadId = log.ContinuidadId,
                EstatusId = log.EstatusId,
                Observaciones = log.Observaciones,
                FechaCreacion = log.FechaCreacion
            };
        }

        public async Task<List<LogContinuidadDto>> GetLogsByContinuidad(int continuidadId)
        {
            var logs = await _logContinuidadRepository.GetLogsByContinuidad(continuidadId);

            return logs.Select(l => new LogContinuidadDto {
                Id = l.Id,
                UsuarioId = l.UsuarioId,
                ContinuidadId = l.ContinuidadId,
                EstatusId = l.EstatusId,
                Observaciones = l.Observaciones,
                FechaCreacion = l.FechaCreacion
            }).ToList();
        }
    }
}
