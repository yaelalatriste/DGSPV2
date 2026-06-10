using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;

namespace DGSP.Module.Catalogos.Application.Services.SMedicos
{
    public interface ICTTipoInsumoService
    {
        Task<List<CTTipoInsumoDto>> GetAllTiposInsumosAsync();
        Task<CTTipoInsumoDto> GetTipoInsumoByIdAsync(int id);
    }
}
