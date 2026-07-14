using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Movimientos;
using DGSP.Module.SMedicos.Domain.Movimientos;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.SMedicos.Persistence.Repositories.Medicamentos.Movimientos
{
    public class MovimientoInventarioRepository : IMovimientoInventarioRepository
    {
        private readonly SMedicosDbContext _db;

        public MovimientoInventarioRepository(SMedicosDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(MovimientoInventario entity)
        {
            await _db.MovimientosInventario.AddAsync(entity);
        }

        public async Task<List<MovimientoInventario>> GetAllMovimientosInventariosAsync()
        {
            return await _db.MovimientosInventario.AsNoTracking().OrderBy(l => l.FechaMovimiento).ToListAsync();
        }
        
        public async Task<List<MovimientoInventario>> GetMovimientosInventariosByLoteAsync(int lote)
        {
            return await _db.MovimientosInventario.Where(m => m.LoteId == lote).OrderBy(l => l.FechaMovimiento).ToListAsync();
        }

        public Task SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
