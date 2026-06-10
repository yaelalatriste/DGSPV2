using DGSP.Module.Estatus.Application.Interfaces.NotasTraspaso;
using DGSP.Module.Estatus.Domain.NotasTraspaso;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Estatus.Persistence.Repositories.NotasTraspaso
{
    public class FlujoNotaTraspasoRepository : IFlujoNotaTraspasoRepository
    {
        private readonly EstatusDbContext _context;

        public FlujoNotaTraspasoRepository(EstatusDbContext context)
        {
            _context = context;
        }

        public async Task<List<FlujoNotaTraspaso>> GetAllFlujosNotasTraspaso()
        {
            return await _context.FlujoNotasTraspaso.AsNoTracking().ToListAsync();
        }

        public async Task<List<FlujoNotaTraspaso>> GetEstatusConsecutivos(int estatus)
        {
            return await _context.FlujoNotasTraspaso.AsNoTracking().Where(fj => fj.EstatusId == estatus).ToListAsync();
        }

    }
}
