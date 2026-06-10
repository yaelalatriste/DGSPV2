using Api.Gateway.Models.Reportes;
using Api.Gateway.Proxies.DGRH.Queries.Empleado;
using Api.Gateway.Proxies.SMedicos.Queries.Reportes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.SMedicos.Reportes
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("smedicos/reportes")]
    public class RConsultaMedicaController : ControllerBase
    {
        private readonly IQReporteConsultaProxy _reportes;
        private readonly ILogger<RConsultaMedicaController> _logger;

        public RConsultaMedicaController(IQReporteConsultaProxy reportes, ILogger<RConsultaMedicaController> logger)
        {
            _reportes = reportes;
            _logger = logger;
        }

        [HttpGet("getAniosOfConsultas")]
        public async Task<IActionResult> GetAniosOfConsultasMedicas()
        {
            try
            {
                _logger.LogInformation("Obteniendo años de consultas médicas...");

                var anios = await _reportes.GetAniosOfConsultas();

                return Ok(anios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener años de consultas médicas");
                return StatusCode(500, new { error = "Error al obtener años de consultas", details = ex.Message });
            }
        }

        [HttpPost("getAllConsultas")]
        public Task<IActionResult> GetAllConsultas([FromBody] FiltrosSmDto filtros) => ExecuteReport(_reportes.GetAllConsultas, filtros);

        [HttpPost("getConsultasMedicas")]
        public Task<IActionResult> GetConsultasMedicas([FromBody] FiltrosSmDto filtros) => ExecuteReport(_reportes.GetConsultasMedicas, filtros);

        [HttpPost("getConsultasOdontologicas")]
        public Task<IActionResult> GetConsultasOdontologicas([FromBody] FiltrosSmDto filtros) => ExecuteReport(_reportes.GetConsultasOdontologicas, filtros);

        [HttpPost("getRevisionEnfermeria")]
        public Task<IActionResult> GetRevisionEnfermeria([FromBody] FiltrosSmDto filtros) => ExecuteReport(_reportes.GetRevisionEnfermeria, filtros);

        /// Método auxiliar para ejecutar consultas de reportes con manejo de errores y logs.
        private async Task<IActionResult> ExecuteReport<T>(Func<FiltrosSmDto, Task<List<T>>> action, FiltrosSmDto filtros)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _logger.LogInformation("Ejecutando reporte con filtros: {@Filtros}", filtros);

                var result = await action(filtros);

                _logger.LogInformation("Reporte obtenido correctamente con {Count} registros", result?.Count ?? 0);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar reporte con filtros {@Filtros}", filtros);
                return StatusCode(500, new
                {
                    error = "Error al procesar el reporte",
                    details = ex.Message
                });
            }
        }
    }
}
