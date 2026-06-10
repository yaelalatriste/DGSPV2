using Api.Gateway.Models.Planeacion.Commands.ActividadesMensuales;
using Api.Gateway.Proxies.Planeacion.Commands.ActividadesMensuales;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Planeacion.ActividadesMensuales.Commands
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("planeacion/actividadesMensuales")]
    public class ActividadMensualController : ControllerBase
    {
        private readonly ICActividadMensualProxy _actMensual;

        public ActividadMensualController(ICActividadMensualProxy actMensual)
        {
            _actMensual = actMensual;
        }

        [HttpPost("createActividad")]
        public async Task<IActionResult> Create([FromBody] ActividadMensualCreateCommand request)
        {
            var actividad = await _actMensual.CreateActividad(request);
            return Ok(actividad);

        }
        
        [HttpPut("updateActividad")]
        public async Task<IActionResult> Update([FromBody] ActividadMensualUpdateCommand request)
        {
            var actividad = await _actMensual.UpdateActividad(request);
            return Ok(actividad);
        }
        
        [HttpPut("deleteActividad")]
        public async Task<IActionResult> Delete([FromBody] ActividadMensualDeleteCommand request)
        {
            var actividad = await _actMensual.DeleteActividad(request);
            return Ok(actividad);
        }
    }
}
