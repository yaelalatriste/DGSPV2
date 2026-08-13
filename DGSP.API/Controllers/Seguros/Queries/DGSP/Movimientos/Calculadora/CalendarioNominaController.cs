using DGSP.Module.Seguros.Application.Services.DGSP.Movimientos.Calculadora;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Seguros.Queries.DGSP.Movimientos.Calculadora
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/seguros/movimientos/[controller]")]
    public class CalendarioNominaController : ControllerBase
    {
        private readonly ICalendarioNominaService _calendarioNominaService;

        public CalendarioNominaController(ICalendarioNominaService calendarioNominaService)
        {
            _calendarioNominaService = calendarioNominaService;
        }

        [HttpGet("getAllCalendario")]
        public async Task<IActionResult> GetAllCalendario()
        {
            var result = await _calendarioNominaService.GetAllCalendarioAsync();
            return Ok(result);
        }

        [HttpGet("getCalendarioById/{id}")]
        public async Task<IActionResult> GetCalendarioById(int id)
        {
            var result = await _calendarioNominaService.GetQuincenaById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("getQuincenasByPeriodo/{fechaInicial}/{fechaFinal}")]
        public async Task<IActionResult> GetQuincenasByPeriodo(string fechaInicial, string fechaFinal)
        {
            var result = await _calendarioNominaService.GetQuincenasByPeriodoAsync(fechaInicial, fechaFinal);
            return Ok(result);
        }
    }
}
