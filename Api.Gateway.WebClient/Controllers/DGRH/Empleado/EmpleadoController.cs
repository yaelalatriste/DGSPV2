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

namespace Api.Gateway.WebClient.Controllers.DGRH.Empleado
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("dgrh/empleado")]
    public class EmpleadoController : ControllerBase
    {
        private readonly IQEmpleadoProxy _empleado;
        private readonly ILogger<EmpleadoController> _logger;

        public EmpleadoController(IQEmpleadoProxy empleado, ILogger<EmpleadoController> logger)
        {
            _empleado = empleado;
            _logger = logger;
        }

        [HttpGet("getEmpleadoByExpediente/{exp}")]
        public async Task<IActionResult> GetEmpleadoByExpediente(int exp)
        {
            try
            {
                _logger.LogInformation("Obteniendo al empleado...");

                var anios = await _empleado.GetEmpleadoByExpediente(exp);

                return Ok(anios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener al empleado");
                return StatusCode(500, new { error = "Error al obtener al empleado", details = ex.Message });
            }
        }

        [HttpGet("getMovimientosEmpleado/{exp}")]
        public async Task<IActionResult> GetMovimientosEmpleado(int exp)
        {
            try
            {
                _logger.LogInformation("Obteniendo al empleado...");

                var anios = await _empleado.GetMovimientosEmpleado(exp);

                return Ok(anios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener al empleado");
                return StatusCode(500, new { error = "Error al obtener al empleado", details = ex.Message });
            }
        }
    }
}
