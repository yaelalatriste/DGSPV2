using DGSP.Shared.Contracts.Commands.Catalogos.SMedicos.Consultorios;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Catalogos.Commands.SMedicos
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/catalogos/Consultorios")]
    public class CTConsultorioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CTConsultorioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("createCTConsultorio")]
        public async Task<IActionResult> CreateConsultorio([FromBody] RegistrarCTConsultorioCommand command)
        {
            var Consultorio = await _mediator.Send(command);

            return Ok(Consultorio);
        }

        [HttpPut]
        [Route("updateCTConsultorio")]
        public async Task<IActionResult> UpdateConsultorio([FromBody] ActualizarCTConsultorioCommand command)
        {
            var Consultorio = await _mediator.Send(command);

            return Ok(Consultorio);
        }
    }
}
