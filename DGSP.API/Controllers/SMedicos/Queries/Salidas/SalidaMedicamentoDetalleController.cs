using DGSP.Module.SMedicos.Application.Services.Medicamentos.Salidas;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.SMedicos.Queries.Salidas
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/smedicos/[controller]")]
    public class SalidaMedicamentoDetalleController : ControllerBase
    {
        private readonly ISalidaMedicamentoDetalleService _salidaMedicamentoDetalleService;

        public SalidaMedicamentoDetalleController(ISalidaMedicamentoDetalleService salidaMedicamentoDetalleService)
        {
            _salidaMedicamentoDetalleService = salidaMedicamentoDetalleService;
        }

        [HttpGet]
        [Route("getDetallesBySalida/{salida}")]
        public async Task<IActionResult> GetDetallesBySalidaAsync(int salida)
        {
            var detalles = await _salidaMedicamentoDetalleService.GetDetallesBySalidaAsync(salida);

            return Ok(detalles);
        }
        
        [HttpGet]
        [Route("getDetalleById/{id}")]
        public async Task<IActionResult> GetDetalleById(int id)
        {
            var detalles = await _salidaMedicamentoDetalleService.GetDetalleByIdAsync(id);

            return Ok(detalles);
        }
    }
}
