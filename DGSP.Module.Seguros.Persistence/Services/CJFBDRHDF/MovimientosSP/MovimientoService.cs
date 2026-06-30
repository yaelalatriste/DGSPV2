using DGSP.Module.DGRH.Application.Interfaces.Seguros.Movimientos;
using DGSP.Module.Seguros.Application.Services.CJFBDRHDF.SGMM;
using DGSP.Shared.Contracts.DTOs.DGRH.Seguros;

namespace DGSP.Module.DGRH.Persistence.Services.Seguros.MovimientosSP
{
    public class MovimientoService : IMovimientoService
    {
        private readonly IMovimientoRepository _movimientoRepository;

        public MovimientoService(IMovimientoRepository movimientoRepository)
        {
            _movimientoRepository = movimientoRepository;
        }

        public async Task<List<MovimientoDto>> GetAllMovimientosAsync()
        {
            var movimientos = await _movimientoRepository.GetAllMovimientosAsync();
            
            return movimientos.Select(m => new MovimientoDto { 
                fiIdMov = m.fiIdMov,
                fcDescMov = m.fcDescMov
            }).ToList();
        }

        public async Task<MovimientoDto> GetMovimientosByIdAsync(int id)
        {
            var movimiento = await _movimientoRepository.GetMovimientosByIdAsync(id);
                
            return new MovimientoDto
            {
                fiIdMov = movimiento.fiIdMov,
                fcDescMov = movimiento.fcDescMov
            } ?? new MovimientoDto();
        }
    }
}
