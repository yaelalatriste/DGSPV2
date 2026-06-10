using Api.Gateway.Models.Seguros.Commands.Continuidades.Continuidad;
using Api.Gateway.Proxies.Seguros.Commands.Continuidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Seguros.Continuidades.Commands.Continuidades
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("seguros/scontinuidades")]
    public class CContinuidadesController : ControllerBase
    {
        private readonly ICContinuidadesProxy _scontinuidad;

        public CContinuidadesController(ICContinuidadesProxy scontinuidad)
        {
            _scontinuidad = scontinuidad;
        }

        [Route("createScontinuidad")]
        [HttpPost]
        public async Task<IActionResult> CreateSContinuidad([FromBody] ContinuidadCreateCommand command)
        {
            var expediente = await _scontinuidad.CreateSContinuidades(command);
            if (expediente != null)
            {
                return Ok(expediente);
            }

            return BadRequest();
        }

        [Route("updateScontinuidad")]
        [HttpPut]
        public async Task<IActionResult> UpdateSContinuidad([FromBody] ContinuidadUpdateCommand command)
        {
            var expediente = await _scontinuidad.UpdateSContinuidades(command);
            if (expediente != null)
            {
                return Ok(expediente);
            }

            return BadRequest();
        }
    }
}
