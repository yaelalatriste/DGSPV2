using DGSP.Module.Seguros.Application.Interfaces.Logs;
using DGSP.Module.Seguros.Domain.DGSP.Continuidades;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Seguros.Persistence.Repositories.Logs
{
    public class LogContinuidadRepository : ILogContinuidadRepository
    {
        private readonly SegurosDbContext _context;

        public LogContinuidadRepository(SegurosDbContext context)
        {
            _context = context;
        }

        public async Task<List<LogContinuidad>> GetLogsByContinuidad(int continuidadId)
        {
            return await _context.LogsContinuidades.AsNoTracking().Where(dt => dt.ContinuidadId == continuidadId).OrderByDescending(l => l.FechaCreacion).ToListAsync();
        }

        public async Task AddLogContinuidadAsync(LogContinuidad command)
        {
            await _context.LogsContinuidades.AddAsync(command);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
