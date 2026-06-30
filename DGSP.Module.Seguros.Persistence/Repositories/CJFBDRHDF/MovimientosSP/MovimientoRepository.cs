using DGSP.Module.DGRH.Application.Interfaces.Seguros.Movimientos;
using DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.Movimientos;
using DGSP.Module.Seguros.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.DGRH.Persistence.Repositories.Seguros.MovimientosSP
{
    public class MovimientoRepository: IMovimientoRepository
    {
        private readonly SegurosSGMMContext _context;

        public MovimientoRepository(SegurosSGMMContext context)
        {
            _context = context;
        }

        public async Task<List<Movimiento>> GetAllMovimientosAsync()
        {
            var movimientos = await _context.CTMovimientos.AsNoTracking().Select(m => new Movimiento
            {
                fiIdMov = m.fiIdMov,
                fcDescMov = m.fcDescMov
            }).ToListAsync();

            return movimientos;
        }

        public async Task<Movimiento> GetMovimientosByIdAsync(int id)
        {
            var movimiento = await _context.CTMovimientos.AsNoTracking().Where(m => m.fiIdMov == id).Select(m => new Movimiento
            {
                fiIdMov = m.fiIdMov,
                fcDescMov = m.fcDescMov
            }).FirstOrDefaultAsync();

            return movimiento ?? new Movimiento();
        }
    }
}
