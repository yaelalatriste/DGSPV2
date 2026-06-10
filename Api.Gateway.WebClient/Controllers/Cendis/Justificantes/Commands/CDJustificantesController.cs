using Api.Gateway.Models.Cendis.Commands.DetallesJustificantes;
using Api.Gateway.Proxies.Cendis.Commands.DJustificantes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cendis.Api.Controllers.Justificantes.Commands
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("cendis/detalleJustificantes")]
    public class CDJustificantesController : Controller
    {
        private readonly ICDJustificanteProxy _justificante;

        public CDJustificantesController(ICDJustificanteProxy justificante)
        {
            _justificante = justificante;
        }

        [Consumes("multipart/form-data")]
        [Route("createDJustificante")]
        [HttpPost]
        public async Task<IActionResult> CreateDJustificante([FromForm] DJustificanteCreateCommand command)
        {
            var dJustificante = await _justificante.CreateDJustificante(command);

            return Ok(dJustificante);
        }
        
        [Consumes("multipart/form-data")]
        [Route("updateDJustificante")]
        [HttpPut]
        public async Task<IActionResult> UpdateDJustificante([FromForm] DJustificanteUpdateCommand command)
        {
            var dJustificante = await _justificante.UpdateDJustificante(command);

            return Ok(dJustificante);
        }
        
        [Route("deleteDJustificantes")]
        [HttpPut]
        public async Task<IActionResult> DeleteDJustificante([FromBody] DJustificanteDeleteCommand command)
        {
            var dJustificante = await _justificante.DeleteDJustificante(command);

            return Ok(dJustificante);
        }


    }
}
