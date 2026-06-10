using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Catalogos;

namespace DGSP.Module.SMedicos.Application.Interfaces.Siacom.TipoConsultaDetalle
{
    public interface ITCDetalleQueryService
    {
        Task<List<CTTipoConsultaDetalleDto>> GetAllTiposConsultaDetalle();
        Task<CTTipoConsultaDetalleDto> GetTipoConsultaDetalleById(int id);
    }
}
