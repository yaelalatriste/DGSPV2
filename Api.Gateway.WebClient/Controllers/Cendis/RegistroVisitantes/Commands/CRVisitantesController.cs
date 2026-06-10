using Api.Gateway.Models.Cendis.Commands.RegistroVisitantes;
using Api.Gateway.Proxies.Cendis.Commands.RegistroVisitantes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Cendis.RegistroVisitantes.Commands
{
    [ApiController]
    [Route("cendis/registroVisitantes")]
    public class CRVisitantesController : Controller
    {
        private readonly ICRVisitantesCendiProxy _visitantes;

        public CRVisitantesController(ICRVisitantesCendiProxy visitantes)
        {
            _visitantes = visitantes;
        }

        [Route("createRegistro")]
        [HttpPost]
        public async Task<IActionResult> CreateJustificante([FromBody] RegistroVisitantesCreateCommand command)
        {
            var expediente = await _visitantes.CreateRegistro(command);
            if (expediente != null)
            {
                return Ok(expediente);
            }

            return BadRequest();
        }
        
        [Route("updateRegistro")]
        [HttpPut]
        public async Task<IActionResult> UpdateJustificante([FromBody] RegistroVisitantesUpdateCommand command)
        {
            var expediente = await _visitantes.UpdateRegistro(command);
            if (expediente != null)
            {
                return Ok(expediente);
            }

            return BadRequest();
        }
    }
}
