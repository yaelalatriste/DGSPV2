using Catalogos.Persistence.Database;
using DGSP.Module.Catalogos.Application.Interfaces.SMedicos;
using DGSP.Module.Catalogos.Domain.SMedicos;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Catalogos.Persistence.Repositories.SMedicos
{
    public class CTUnidadRepository : ICTUnidadRepository
    {
        private readonly CatalogoDbContext _context;

        public CTUnidadRepository(CatalogoDbContext context)
        {
            _context = context;
        }

        public async Task<List<CTUnidad>> GetAllUnidadesAsync()
        {
            return await _context.CTUnidades.AsNoTracking().ToListAsync();
        }

        public async Task<CTUnidad> GetUnidadByIdAsync(int id)
        {
            return await _context.CTUnidades.AsNoTracking().Where(c => c.Id == id).FirstAsync();
        }
    }
}
