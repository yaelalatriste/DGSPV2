using Catalogos.Persistence.Database;
using DGSP.Module.Catalogos.Application.Interfaces.Generales;
using DGSP.Module.Catalogos.Domain.Generales;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Catalogos.Persistence.Repositories.Generales
{
    public class CTAreaRepository : ICTAreaRepository
    {
        private readonly CatalogoDbContext _context;

        public CTAreaRepository(CatalogoDbContext context)
        {
            _context = context;
        }

        public async Task<List<CTArea>> GetAllAreasAsync()
        {
            return await _context.CTAreas.AsNoTracking().ToListAsync();
        }

        public async Task<CTArea> GetAreaByIdAsync(int id)
        {
            return await _context.CTAreas.Where(m => m.Id == id).AsNoTracking().FirstAsync();
        }
    }
}
