using DGSP.Module.Estatus.Application.Interfaces.NotasTraspaso;
using DGSP.Module.Estatus.Domain.NotasTraspaso;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Estatus.Persistence.Repositories.NotasTraspaso
{
    public class EstatusNotasTraspasoRepository : IEstatusNotasTraspasoRepository
    {
        private readonly EstatusDbContext _context;

        public EstatusNotasTraspasoRepository(EstatusDbContext context)
        {
            _context = context;
        }

        public async Task<List<ENotaTraspaso>> GetAllEstatus()
        {
            return await _context.ENotasTraspaso.AsNoTracking().ToListAsync();
        }

        public async Task<ENotaTraspaso> GetEstatusById(int id)
        {
            return await _context.ENotasTraspaso.AsNoTracking().Where(c => c.Id == id).FirstOrDefaultAsync() ?? new ENotaTraspaso();
        }
    }
}
