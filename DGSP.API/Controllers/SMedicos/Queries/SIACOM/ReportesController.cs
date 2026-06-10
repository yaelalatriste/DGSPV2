using DGSP.Module.SMedicos.Application.Interfaces.Reportes;
using DGSP.Module.SMedicos.Application.Queries.Reportes;
using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Reportes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMedicos.Services.Queries.Queries.Reportes;

namespace DGSP.API.Controllers.SMedicos.Queries.SIACOM
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/smedicos/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IRServiciosMedicosQueryService _reporte;
        private readonly IDashboardConsultasService _dashboardConsultasService;

        public ReportesController(IRServiciosMedicosQueryService reporte, IDashboardConsultasService dashboardConsultasService)
        {
            _reporte = reporte;
            _dashboardConsultasService = dashboardConsultasService;
        }

        [HttpGet]
        [Route("getAniosOfConsultas")]
        public async Task<List<int>> GetAniosOfConsultas()
        {
            var anios = await _reporte.GetAniosOfConsultas();
            return anios;
        }
        
        [HttpPost]
        [Route("getAllConsultas")]
        public async Task<List<ResumenTipoConsultaDto>> GetReporteTiposConsultasAsync(FiltrosSmDto filtros)
        {
            var reporteTC = await _reporte.GetReporteGeneralTiposConsultasAsync(filtros);

            return reporteTC;
        }
       
        [HttpPost]
        [Route("dashboardSiacom")]
        public async Task<IActionResult> ObtenerDashboard([FromBody] FiltrosSmDto request)
         {
            var response = await _dashboardConsultasService.ObtenerDashboardAsync(request);
            return Ok(response);
        }
    }
}
