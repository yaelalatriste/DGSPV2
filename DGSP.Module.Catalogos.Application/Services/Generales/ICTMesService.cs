using DGSP.Shared.Contracts.DTOs.Catalogos.Generales;

namespace DGSP.Module.Catalogos.Application.Services.Generales
{
    public interface ICTMesService
    {
        Task<List<CTMesDto>> GetAllMesesAsync();
        Task<CTMesDto> GetMesByIdAsync(int id);
    }
}
