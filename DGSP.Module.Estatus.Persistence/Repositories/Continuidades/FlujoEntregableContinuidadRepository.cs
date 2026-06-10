using DGSP.Module.Estatus.Application.Interfaces.Continuidades;
using DGSP.Module.Estatus.Domain.Continuidade;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Estatus.Persistence.Repositories.Continuidades
{
    public class FlujoEntregableContinuidadRepository : IFlujoEntregableContinuidadRepository
    {
        private readonly EstatusDbContext _context;

        public FlujoEntregableContinuidadRepository(EstatusDbContext context)
        {
            _context = context;
        }

        public async Task<List<FlujoEntregableContinuidad>> GetAllFlujosEntregablesContinuidades()
        {
            return await _context.FlujoEntregablesContinuidades.AsNoTracking().ToListAsync();
        }

        public async Task<List<FlujoEntregableContinuidad>> GetEstatusConsecutivos(int estatus)
        {
            return await _context.FlujoEntregablesContinuidades.AsNoTracking().Where(fj => fj.EstatusId == estatus).ToListAsync();
        }

    }
}
