using DGSP.Module.Estatus.Application.Interfaces.Continuidades;
using DGSP.Module.Estatus.Domain;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Estatus.Persistence.Repositories.Continuidades
{
    public class EstatusContinuidadesRepository : IEstatusContinuidadesRepository
    {
        private readonly EstatusDbContext _context;

        public EstatusContinuidadesRepository(EstatusDbContext context)
        {
            _context = context;
        }

        public async Task<List<EstatusContinuidad>> GetAllEstatus()
        {
            return await _context.EstatusContinuidades.AsNoTracking().ToListAsync();
        }

        public async Task<EstatusContinuidad> GetEstatusById(int id)
        {
            return await _context.EstatusContinuidades.AsNoTracking().Where(c => c.Id == id).FirstOrDefaultAsync() ?? new EstatusContinuidad();
        }
    }
}
