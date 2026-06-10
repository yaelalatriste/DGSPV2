using Catalogos.Persistence.Database;
using DGSP.Module.Catalogos.Application.Interfaces.SMedicos;
using DGSP.Module.Catalogos.Domain.SMedicos;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Catalogos.Persistence.Repositories.SMedicos
{
    public class CTTipoMovimientoRepository : ICTTipoMovimientoRepository
    {
        private readonly CatalogoDbContext _context;

        public CTTipoMovimientoRepository(CatalogoDbContext context)
        {
            _context = context;
        }

        public async Task<List<CTTipoMovimiento>> GetAllTiposMovimientosAsync()
        {
            return await _context.CTTiposMovimientos.AsNoTracking().Where(a => a.Activo).ToListAsync();
        }

        public async Task<CTTipoMovimiento> GetTipoMovimientoByIdAsync(int id)
        {
            return await _context.CTTiposMovimientos.AsNoTracking().Where(c => c.Id == id).FirstAsync();
        }
        
        public async Task<List<CTTipoMovimiento>> GetMovimientosEntradaAsync()
        {
            return await _context.CTTiposMovimientos.AsNoTracking().Where(a => a.Activo && a.Entrada).ToListAsync();
        }
        
        public async Task<List<CTTipoMovimiento>> GetMovimientosSalidaAsync()
        {
            return await _context.CTTiposMovimientos.AsNoTracking().Where(a => a.Activo && a.Salida).ToListAsync();
        }
    }
}
