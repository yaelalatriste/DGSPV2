using Api.Gateway.Models.Seguros.Commands.Continuidades.CEntregables;
using Api.Gateway.Proxies.Seguros.Commands.Continuidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Seguros.Continuidades.Commands.CEntregables
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("seguros/continuidades")]
    public class CEntregableController : Controller
    {
        private readonly ICContinuidadesProxy _continuidad;

        public CEntregableController(ICContinuidadesProxy continuidad)
        {
            _continuidad = continuidad;
        }

        [Consumes("multipart/form-data")]
        [Route("createEntregable")]
        [HttpPost]
        public async Task<IActionResult> CreateEntregable([FromForm] CEntregableCreateCommand command)
        {
            var entregable = await _continuidad.CreateEntregable(command);
            if (entregable != null)
            {
                return Ok(entregable);
            }

            return BadRequest();
        }
        
        [Consumes("multipart/form-data")]
        [Route("updateEntregable")]
        [HttpPut]
        public async Task<IActionResult> updateEntregable([FromForm] CEntregableUpdateCommand command)
        {
            var entregable = await _continuidad.UpdateEntregable(command);
            if (entregable != null)
            {
                return Ok(entregable);
            }

            return BadRequest();
        }
    }
}
