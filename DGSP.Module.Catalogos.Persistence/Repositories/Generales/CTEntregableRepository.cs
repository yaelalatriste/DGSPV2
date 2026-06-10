using Catalogos.Persistence.Database;
using DGSP.Module.Catalogos.Application.Interfaces.Generales;
using DGSP.Module.Catalogos.Domain.Generales;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Catalogos.Persistence.Repositories.Generales
{
    public class CTEntregableRepository : ICTEntregableRepository
    {
        private readonly CatalogoDbContext _context;

        public CTEntregableRepository(CatalogoDbContext context)
        {
            _context = context;
        }

        public async Task<List<CTEntregable>> GetAllEntregablesAsync()
        {
            return await _context.CTEntregables.AsNoTracking().ToListAsync();
        }

        public async Task<CTEntregable> GetEntregableByIdAsync(int id)
        {
            return await _context.CTEntregables.Where(m => m.Id == id).AsNoTracking().FirstAsync();
        }
    }
}
