using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Catalogos;

namespace DGSP.Module.SMedicos.Application.Interfaces.Siacom.TipoServicios
{
    public interface ITipoServicioQueryService
    {
        Task<List<CTTipoServicioDto>> GetAllTiposServicios();
        Task<CTTipoServicioDto> GetTipoServicioById(int id);
    }
}
