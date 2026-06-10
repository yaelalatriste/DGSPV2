using Api.Gateway.Models.DG.Commands.AEntregable;
using Api.Gateway.Proxies.DG.Commands.AEntregables;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.DG.Entregables.Acuerdos.Commands
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("dg/entregablesAcuerdos")]
    public class CAcuerdosController : ControllerBase
    {
        private readonly ICAEntregableProxy _entregables;

        public CAcuerdosController(ICAEntregableProxy entregables)
        {
            _entregables = entregables;
        }

        [Consumes("multipart/form-data")]
        [Route("createAEntregable")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AEntregableCreateCommand request)
        {
            var entregable = await _entregables.CreateAEntregable(request);
            
            return Ok(entregable);
        }

        [Consumes("multipart/form-data")]
        [Route("updateAEntregable")]
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] AEntregableUpdateCommand request)
        {
            var entregable = await _entregables.UpdateAEntregable(request);

            return Ok(entregable);
        }

        [Route("deleteAEntregable")]
        [HttpPut]
        public async Task<IActionResult> Delete([FromBody] AEntregableDeleteCommand request)
        {
            var entregable = await _entregables.DeleteAEntregable(request);
            
            return Ok(entregable);
        }
    }
}
