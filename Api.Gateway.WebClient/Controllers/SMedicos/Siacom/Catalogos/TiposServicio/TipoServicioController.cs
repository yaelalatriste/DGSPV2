using Api.Gateway.Proxies.SMedicos.Queries.Siacom.TiposServicio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.SMedicos.Siacom.Catalogos.TiposServicio
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("smedicos/tiposServicio")]
    public class TipoServicioController : ControllerBase
    {
        private readonly IQCTTipoServicioProxy _tipoServicio;

        public TipoServicioController(IQCTTipoServicioProxy tipoServicio)
        {
            _tipoServicio = tipoServicio;
        }

        [HttpGet]
        [Route("getAllTiposServicio")]
        public async Task<IActionResult> GetAllTiposServicios()
        {
            var tiposConsultas = await _tipoServicio.GetAllTiposServicios();

            return Ok(tiposConsultas);
        }


        [HttpGet]
        [Route("getTipoServicioById/{id}")]
        public async Task<IActionResult> GetTipoConsultaById(int id)
        {
            var tipoConsulta = await _tipoServicio.GetTipoServicioById(id);

            return Ok(tipoConsulta);
        }
    }
}
