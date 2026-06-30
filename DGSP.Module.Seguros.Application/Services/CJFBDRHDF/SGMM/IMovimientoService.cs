using DGSP.Shared.Contracts.DTOs.DGRH.Seguros;

namespace DGSP.Module.Seguros.Application.Services.CJFBDRHDF.SGMM
{
    public interface IMovimientoService
    {
        public Task<List<MovimientoDto>> GetAllMovimientosAsync();
        public Task<MovimientoDto> GetMovimientosByIdAsync(int id);
    }
}
