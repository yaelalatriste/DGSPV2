using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Dashboards;
using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Reportes;

namespace DGSP.Module.SMedicos.Application.Interfaces.Reportes
{
    public interface IDashboardConsultasService
    {
        Task<DashboardConsultasResponseDto> ObtenerDashboardAsync(FiltrosSmDto request);
    }
}
