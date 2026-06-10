using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;

namespace DGSP.Module.Catalogos.Application.Services.SMedicos
{
    public interface ICTUnidadService
    {
        Task<List<CTUnidadDto>> GetAllUnidadesAsync();
        Task<CTUnidadDto> GetUnidadByIdAsync(int id);
    }
}
