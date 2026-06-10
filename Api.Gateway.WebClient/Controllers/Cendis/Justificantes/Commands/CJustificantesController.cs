using Api.Gateway.Models.Cendis.Commands.Justificantes;
using Api.Gateway.Proxies.Cendis.Commands.Justificantes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cendis.Api.Controllers.Justificantes.Commands
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("cendis/justificantes")]
    public class CJustificantesController : Controller
    {
        private readonly ICJustificanteProxy _justificantes;

        public CJustificantesController(ICJustificanteProxy justificantes)
        {
            _justificantes = justificantes;
        }

        [Route("createJustificante")]
        [HttpPost]
        public async Task<IActionResult> CreateJustificante([FromBody] JustificanteCreateCommand command)
        {
            var justificante = await _justificantes.CreateJustificante(command);
            if (justificante != null)
            {
                return Ok(justificante);
            }

            return BadRequest();
        }
        
        [Route("updateJustificante")]
        [HttpPut]
        public async Task<IActionResult> UpdateJustificante([FromBody] JustificanteUpdateCommand command)
        {
            var justificante = await _justificantes.UpdateJustificante(command);

            if (justificante != null)
            {
                return Ok(justificante);
            }

            return BadRequest();
        }
        
        [Route("deleteJustificante")]
        [HttpPut]
        public async Task<IActionResult> DeleteJustificante([FromBody] JustificanteDeleteCommand command)
        {
            var justificante = await _justificantes.DeleteJustificante(command);
            if (justificante != null)
            {
                return Ok(justificante);
            }

            return BadRequest();
        }


    }
}
