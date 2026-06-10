using DGSP.Module.Catalogos.Domain.SMedicos;

namespace DGSP.Module.Catalogos.Application.Interfaces.SMedicos
{
    public interface ICTTipoMovimientoRepository
    {
        Task<List<CTTipoMovimiento>> GetAllTiposMovimientosAsync();
        Task<CTTipoMovimiento> GetTipoMovimientoByIdAsync(int id);
        Task<List<CTTipoMovimiento>> GetMovimientosEntradaAsync();
        Task<List<CTTipoMovimiento>> GetMovimientosSalidaAsync();
    }
}
