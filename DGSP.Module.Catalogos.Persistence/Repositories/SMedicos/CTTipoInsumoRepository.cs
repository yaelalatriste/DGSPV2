using Catalogos.Persistence.Database;
using DGSP.Module.Catalogos.Application.Interfaces.SMedicos;
using DGSP.Module.Catalogos.Domain.SMedicos;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Catalogos.Persistence.Repositories.SMedicos
{
    public class CTTipoInsumoRepository : ICTTipoInsumoRepository
    {
        private readonly CatalogoDbContext _context;

        public CTTipoInsumoRepository(CatalogoDbContext context)
        {
            _context = context;
        }

        public async Task<List<CTTipoInsumo>> GetAllTiposInsumosAsync()
        {
            return await _context.CTTiposInsumos.AsNoTracking().Where(m => m.Activo == true).ToListAsync();
        }

        public async Task<CTTipoInsumo> GetTipoInsumoByIdAsync(int id)
        {
            return await _context.CTTiposInsumos.AsNoTracking().Where(c => c.Id == id && c.Activo == true).FirstAsync();
        }

    }
}
