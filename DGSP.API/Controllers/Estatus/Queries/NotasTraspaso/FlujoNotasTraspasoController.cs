using DGSP.Module.Estatus.Application.Services.NotasTraspaso;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Estatus.Queries.NotasTraspaso
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/estatus/[controller]")]
    public class FlujoNotasTraspasoController : ControllerBase
    {
        private readonly IFlujoNotaTraspasoService _fnota;

        public FlujoNotasTraspasoController(IFlujoNotaTraspasoService fnota)
        {
            _fnota = fnota;
        }

        [HttpGet]
        [Route("getAllFlujosNotas")]
        public async Task<IActionResult> GetAllFlujosJustificantes()
        {
            var estatus = await _fnota.GetAllFlujosNotasTraspaso();

            return Ok(estatus);
        }

        [HttpGet]
        [Route("getEstatusConsecutivoNota/{estatus}")]
        public async Task<IActionResult> GetEstatusConsecutivoNota(int estatus)
        {
            var consecutivos = await _fnota.GetEstatusConsecutivos(estatus);

            return Ok(consecutivos);
        }

    }
}
