using DGSP.Module.Estatus.Application.Services.NotasTraspaso;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Estatus.Queries.Continuidades
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/estatus/[controller]")]
    public class FlujoEntregableContinuidadController : ControllerBase
    {
        private readonly IFlujoEntregableContinuidadService _flujoEntregables;

        public FlujoEntregableContinuidadController(IFlujoEntregableContinuidadService flujoEntregables)
        {
            _flujoEntregables = flujoEntregables;
        }

        [HttpGet]
        [Route("getAllFlujosEntregablesContinuidades")]
        public async Task<IActionResult> GetAllFlujosContinuidades()
        {
            var estatus = await _flujoEntregables.GetAllFlujosEntregablesContinuidades();

            return Ok(estatus);
        }

        [HttpGet]
        [Route("getEstatusConsecutivoContinuidad/{estatus}")]
        public async Task<IActionResult> GetEstatusConsecutivoContinuidad(int estatus)
        {
            var consecutivos = await _flujoEntregables.GetEstatusConsecutivos(estatus);

            return Ok(consecutivos);
        }

    }
}
