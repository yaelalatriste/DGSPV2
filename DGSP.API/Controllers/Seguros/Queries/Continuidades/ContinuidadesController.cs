using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Seguros.Queries.Continuidades
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
