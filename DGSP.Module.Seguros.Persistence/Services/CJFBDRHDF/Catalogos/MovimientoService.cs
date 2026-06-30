using DGSP.Module.Seguros.Application.Services.CJFBDRHDF.SGMM;
using DGSP.Shared.Contracts.DTOs.DGRH.Seguros;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Seguros.Persistence.Services.CJFBDRHDF.Catalogos
{
    public class MovimientoService : IMovimientoService
    {
        private readonly SegurosSGMMContext _context;

        public MovimientoService(SegurosSGMMContext context)
        {
            _context = context;
        }

        public async Task<List<MovimientoDto>> GetAllMovimientosAsync()
        {
            var movimientos = await _context.CTMovimientos.AsNoTracking().Select(m => new MovimientoDto { 
                fiIdMov = m.fiIdMov,
                fcDescMov = m.fcDescMov
            }).ToListAsync();

            return movimientos;
        }

        public async Task<MovimientoDto> GetMovimientosByIdAsync(int id)
        {
            var movimiento = await _context.CTMovimientos.AsNoTracking().Where(m => m.fiIdMov == id).Select(m => new MovimientoDto
            {
                fiIdMov = m.fiIdMov,
                fcDescMov = m.fcDescMov
            }).FirstOrDefaultAsync();

            return movimiento ?? new MovimientoDto();
        }
    }
}
