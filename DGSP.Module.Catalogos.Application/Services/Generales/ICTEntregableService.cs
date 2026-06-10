using DGSP.Shared.Contracts.DTOs.Catalogos.Generales;

namespace DGSP.Module.Catalogos.Application.Services.Generales
{
    public interface ICTEntregableService
    {
        Task<List<CTEntregableDto>> GetAllEntregablesAsync();
        Task<CTEntregableDto> GetEntregableByIdAsync(int id);
    }
}
