using Api.Gateway.Proxies.SMedicos.Queries.Siacom.TiposConsultaDetalle;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.SMedicos.Siacom.Catalogos.TiposConsultaDetalle
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("smedicos/tiposConsultaDetalle")]
    public class TipoConsultaDetalleController : ControllerBase
    {
        private readonly IQCTTipoConsultaDetalleProxy _tcdetalle;
        public TipoConsultaDetalleController(IQCTTipoConsultaDetalleProxy tcdetalle)
        {
            _tcdetalle = tcdetalle;
        }

        [HttpGet]
        [Route("getAllTCDetalle")]
        public async Task<IActionResult> GetAlltcDetalles()
        {
            var tcDetalles = await _tcdetalle.GetAllTiposConsultaDetalle();

            return Ok(tcDetalles);
        }

        [HttpGet]
        [Route("getTCDetalleById/{id}")]
        public async Task<IActionResult> GettcDetalleById(int id)
        {
            var tcDetalles = await _tcdetalle.GetTipoConsultaDetalleById(id);

            return Ok(tcDetalles);
        }
    }
}
