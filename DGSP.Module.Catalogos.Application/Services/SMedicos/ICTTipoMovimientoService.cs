using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;

namespace DGSP.Module.Catalogos.Application.Services.SMedicos
{
    public interface ICTTipoMovimientoService
    {
        Task<List<CTTipoMovimientoDto>> GetAllTiposMovimientosAsync();
        Task<CTTipoMovimientoDto> GetTipoMovimientoByIdAsync(int id);
        Task<List<CTTipoMovimientoDto>> GetMovimientosEntradaAsync();
        Task<List<CTTipoMovimientoDto>> GetMovimientosSalidaAsync();
    }
}
