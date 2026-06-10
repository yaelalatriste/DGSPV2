using DGSP.Shared.Contracts.DTOs.Catalogos.Generales;

namespace DGSP.Module.Catalogos.Application.Services.Generales
{
    public interface ICTAreaService
    {
        Task<List<CTAreaDto>> GetAllAreasAsync();
        Task<CTAreaDto> GetAreaByIdAsync(int id);
    }
}
