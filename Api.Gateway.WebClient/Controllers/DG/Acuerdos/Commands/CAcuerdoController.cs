using System.Threading.Tasks;
using Api.Gateway.Models.DG.Commands.Acuerdos;
using Api.Gateway.Proxies.DG.Commands.Acuerdos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.WebClient.Controllers.DG.Acuerdos.Commands
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("dg/acuerdos")]
    public class CAcuerdoController : Controller
    {
        private readonly ICAcuerdoProxy _acuerdos;

        public CAcuerdoController(ICAcuerdoProxy acuerdos)
        {
            _acuerdos = acuerdos;
        }

        [Route("createAcuerdo")]
        [HttpPost]
        public async Task<IActionResult> CreateAcuerdo([FromBody] AcuerdoCreateCommand command)
        {
            var acuerdo = await _acuerdos.CreateAcuerdo(command);
            
            if (acuerdo != null)
            {
                return Ok(acuerdo);
            }
            
            return BadRequest();
        }

        [Route("updateAcuerdo")]
        [HttpPut]
        public async Task<IActionResult> UpdateAcuerdo([FromBody] AcuerdoUpdateCommand command)
        {
            var acuerdo = await _acuerdos.UpdateAcuerdo(command);
            if (acuerdo != null)
            {
                return Ok(acuerdo);
            }

            return BadRequest();
        }
        
        [Route("deleteAcuerdo")]
        [HttpPut]
        public async Task<IActionResult> DeleteAcuerdo([FromBody] AcuerdoDeleteCommand command)
        {
            var acuerdo = await _acuerdos.DeleteAcuerdo(command);
            if (acuerdo != null)
            {
                return Ok(acuerdo);
            }

            return BadRequest();
        }


    }
}
