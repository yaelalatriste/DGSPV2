using DGSP.Module.Catalogos.Domain.Generales;

namespace DGSP.Module.Catalogos.Application.Interfaces.Generales
{
    public interface ICTAreaRepository
    {
        Task<List<CTArea>> GetAllAreasAsync();
        Task<CTArea> GetAreaByIdAsync(int id);
    }
}
