using System.Threading.Tasks;
using Api.Gateway.Models.Expedientes.Commands;
using Api.Gateway.Proxies.Prestaciones.Commands.Expedientes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.WebClient.Controllers.Prestaciones.Expedientes.Commands
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("prestaciones/expedientes")]
    public class CExpedientesController : Controller
    {
        private readonly ICExpedientePrestacionesProxy _expediente;

        public CExpedientesController(ICExpedientePrestacionesProxy expediente)
        {
            _expediente = expediente;
        }

        [Consumes("multipart/form-data")]
        [Route("createExpediente")]
        [HttpPost]
        public async Task<IActionResult> CreateJustificante([FromForm] ExpedienteCreateCommand command)
        {
            var expediente = await _expediente.CreateExpediente(command);
            if (expediente != null)
            {
                return Ok(expediente);
            }

            return BadRequest();
        }

        [Consumes("multipart/form-data")]
        [Route("updateExpediente")]
        [HttpPut]
        public async Task<IActionResult> UpdateJustificante([FromForm] ExpedienteUpdateCommand command)
        {
            var expediente = await _expediente.UpdateExpediente(command);
            if (expediente != null)
            {
                return Ok(expediente);
            }

            return BadRequest();
        }
        
        [Route("deleteExpediente")]
        [HttpPut]
        public async Task<IActionResult> DeleteJustificante([FromBody] ExpedienteDeleteCommand command)
        {
            var expediente = await _expediente.DeleteExpediente(command);
            if (expediente != null)
            {
                return Ok(expediente);
            }

            return BadRequest();
        }


    }
}
