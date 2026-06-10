using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Catalogos;

namespace DGSP.Module.SMedicos.Application.Interfaces.Siacom.TipoConsulta
{
    public interface ITipoConsultaQueryService
    {
        Task<List<CTTipoConsultaDto>> GetAllTiposConsultas();
        Task<CTTipoConsultaDto> GetTipoConsultaById(int id);
    }
}
