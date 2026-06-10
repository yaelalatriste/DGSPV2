using Api.Gateway.Proxies.Planeacion.Commands.Entregables;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Planeacion.Service.EventHandler.Commands.EntregablesAM;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Planeacion.Entregables.Commands
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("planeacion/entregablesAM")]
    public class EnregablesAMCommandController : ControllerBase
    {
        private readonly ICEntregablesAMProxy _entregables;
        private readonly IHostingEnvironment _environment;

        public EnregablesAMCommandController(ICEntregablesAMProxy entregables, IHostingEnvironment environment)
        {
            _entregables = entregables;
            _environment = environment;
        }

        [Produces("application/json")]
        [Consumes("multipart/form-data")]
        [Route("createEntregableAM")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EntregableAMCreateCommand request)
        {
            var entregable = await _entregables.CreateActividad(request);

            return Ok(entregable);
        }

        [Produces("application/json")]
        [Consumes("multipart/form-data")]
        [Route("updateEntregableAM")]
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] EntregableAMUpdateCommand request)
        {
            var entregable = await _entregables.UpdateActividad(request);

            return Ok(entregable);
        }

        [Route("deleteEntregableAM")]
        [HttpPut]
        public async Task<IActionResult> Delete([FromBody] EntregableAMDeleteCommand request)
        {
            var entregable = await _entregables.DeleteActividad(request);

            return Ok(entregable);
        }

    }
}
