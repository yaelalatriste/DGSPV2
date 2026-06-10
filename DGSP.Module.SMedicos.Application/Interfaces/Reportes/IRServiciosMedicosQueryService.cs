using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Reportes;

namespace DGSP.Module.SMedicos.Application.Interfaces.Reportes
{
    public interface IRServiciosMedicosQueryService
    {
        Task<List<int>> GetAniosOfConsultas();
        Task<List<ResumenTipoConsultaDto>> GetReporteGeneralTiposConsultasAsync(FiltrosSmDto filtros);
    }
}
