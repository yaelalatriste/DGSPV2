using DGSP.Module.Estatus.Application.Services.NotasTraspaso;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Estatus.Queries.Continuidades
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/estatus/[controller]")]
    public class FlujoContinuidadController : ControllerBase
    {
        private readonly IFlujoContinuidadService _flujoContinuidad;

        public FlujoContinuidadController(IFlujoContinuidadService flujoContinuidad)
        {
            _flujoContinuidad = flujoContinuidad;
        }

        [HttpGet]
        [Route("getAllFlujosContinuidades")]
        public async Task<IActionResult> GetAllFlujosContinuidades()
        {
            var estatus = await _flujoContinuidad.GetAllFlujosContinuidades();

            return Ok(estatus);
        }

        [HttpGet]
        [Route("getEstatusConsecutivoContinuidad/{estatus}")]
        public async Task<IActionResult> GetEstatusConsecutivoContinuidad(int estatus)
        {
            var consecutivos = await _flujoContinuidad.GetEstatusConsecutivos(estatus);

            return Ok(consecutivos);
        }

    }
}
