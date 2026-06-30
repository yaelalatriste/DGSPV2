using DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.Movimientos;

namespace DGSP.Module.DGRH.Application.Interfaces.Seguros.Movimientos
{
    public interface IMovimientoRepository
    {
        public Task<List<Movimiento>> GetAllMovimientosAsync();
        public Task<Movimiento> GetMovimientosByIdAsync(int id);
    }

}