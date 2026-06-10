using DGSP.Module.Estatus.Application.Interfaces.Continuidades;
using DGSP.Module.Estatus.Domain.Continuidade;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Estatus.Persistence.Repositories.Continuidades
{
    public class FlujoContinuidadRepository : IFlujoContinuidadRepository
    {
        private readonly EstatusDbContext _context;

        public FlujoContinuidadRepository(EstatusDbContext context)
        {
            _context = context;
        }

        public async Task<List<FlujoContinuidad>> GetAllFlujosContinuidades()
        {
            return await _context.FlujoContinuidades.AsNoTracking().ToListAsync();
        }

        public async Task<List<FlujoContinuidad>> GetEstatusConsecutivos(int estatus)
        {
            return await _context.FlujoContinuidades.AsNoTracking().Where(fj => fj.EstatusId == estatus).ToListAsync();
        }

    }
}
