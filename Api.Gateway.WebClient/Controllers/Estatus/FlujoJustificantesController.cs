using Api.Gateway.Proxies.Estatus.Queries.FlujoJustificantes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Estatus
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("estatus/flujoJustificante")]
    public class FlujoJustificantesController : ControllerBase
    {
        private readonly IQFlujoJustificantesProxy _fJustificantes;

        public FlujoJustificantesController(IQFlujoJustificantesProxy fJustificantes)
        {
            _fJustificantes = fJustificantes;
        }

        [HttpGet]
        [Route("getAllFlujosJustificantes")]
        public async Task<IActionResult> GetAllFlujosJustificantes()
        {
            var estatus = await _fJustificantes.GetAllFlujosJustificantes();

            return Ok(estatus);
        }

        [HttpGet]
        [Route("getAllFlujoByModulo/{modulo}/{submodulo}")]
        public async Task<IActionResult> GetEstatusById(int modulo, int submodulo)
        {
            var estatus = await _fJustificantes.GetAllFlujoByModulo(modulo, submodulo);

            return Ok(estatus);
        }

        [HttpGet]
        [Route("getFlujoByEstatus/{estatus}/{modulo}/{submodulo}")]
        public async Task<IActionResult> GetFlujoByEstatus(int estatus, int modulo, int submodulo)
        {
            var flujo = await _fJustificantes.GetFlujoByEstatus(estatus, modulo, submodulo);

            return Ok(flujo);
        }


    }
}
