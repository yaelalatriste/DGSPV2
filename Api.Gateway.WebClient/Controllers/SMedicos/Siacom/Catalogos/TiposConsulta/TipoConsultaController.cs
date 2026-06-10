using Api.Gateway.Proxies.SMedicos.Queries.Siacom.TiposConsulta;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.SMedicos.Siacom.Catalogos.TiposConsulta
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("smedicos/tiposConsulta")]
    public class TipoConsultaController : ControllerBase
    {
        private readonly IQCTTipoConsultaProxy _tipoConsulta;

        public TipoConsultaController(IQCTTipoConsultaProxy tipoConsulta)
        {
            _tipoConsulta = tipoConsulta;
        }

        [HttpGet]
        [Route("getAllTiposConsultas")]
        public async Task<IActionResult> GetAllTiposConsultas()
        {
            var tiposConsultas = await _tipoConsulta.GetAllTiposConsultas();

            return Ok(tiposConsultas);
        }


        [HttpGet]
        [Route("getTipoConsultaById/{id}")]
        public async Task<IActionResult> GetTipoConsultaById(int id)
        {
            var tipoConsulta = await _tipoConsulta.GetTipoConsultaById(id);

            return Ok(tipoConsulta);
        }
    }
}
