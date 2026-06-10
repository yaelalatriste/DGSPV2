using DGSP.Module.Catalogos.Domain.Generales;

namespace DGSP.Module.Catalogos.Application.Interfaces.Generales
{
    public interface ICTMesRepository
    {
        Task<List<CTMes>> GetAllMesesAsync();
        Task<CTMes> GetMesByIdAsync(int id);
    }
}
