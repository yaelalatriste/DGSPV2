using DGSP.Module.Catalogos.Domain.SMedicos;

namespace DGSP.Module.Catalogos.Application.Interfaces.SMedicos
{
    public interface ICTTipoInsumoRepository
    {
        Task<List<CTTipoInsumo>> GetAllTiposInsumosAsync();
        Task<CTTipoInsumo> GetTipoInsumoByIdAsync(int id);
    }
}
