using DGSP.Module.Seguros.Application.Services.DGSP.Siniestros.Continuidades;
using DGSP.Module.Seguros.Persistence.Services.DGSP.Siniestros.Continuidades;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Seguros.Queries.DGSP.Siniestros.Continuidades
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/seguros/[controller]")]
    public class ContinuidadesController : ControllerBase
    {
        private readonly IContinuidadService _continuidad;

        public ContinuidadesController(IContinuidadService continuidad)
        {
            _continuidad = continuidad;
        }

        [HttpGet]
        [Route("getAllContinuidades")]
        public async Task<IActionResult> GetAllContinuidades()
        {
            var continuidades = await _continuidad.GetAllContinuidadesAsync();

            return Ok(continuidades);
        }

        [HttpGet("periodo")]
        public async Task<ActionResult<List<ContinuidadDto>>> GetPorPeriodo([FromQuery] DateTime fechaInicio,[FromQuery] DateTime fechaFin)
        {
            if (fechaInicio >= fechaFin)
            {
                return BadRequest("La fecha inicial debe ser menor a la fecha final.");
            }

            var resultado = await _continuidad.GetContinuidadesPorPeriodoAsync(fechaInicio,fechaFin);

            return Ok(resultado);
        }

        [HttpGet]
        [Route("getContinuidadById/{id}")]
        public async Task<IActionResult> GetContinuidadById(int id)
        {
            var continuidades = await _continuidad.GetContinuidadByIdAsync(id);

            return Ok(continuidades);
        }
       
        [HttpGet]
        [Route("getContinuidadesByEstatus/{id}")]
        public async Task<IActionResult> GetContinuidadesByEstatus(int id)
        {
            var continuidades = await _continuidad.GetContinuidadesByEstatus(id);

            return Ok(continuidades);
        }
    }
}
