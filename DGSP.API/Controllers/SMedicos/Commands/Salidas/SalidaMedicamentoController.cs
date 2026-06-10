using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Salidas;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.SMedicos.Commands.Salidas
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/smedicos/[controller]")]
    public class SalidaMedicamentoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalidaMedicamentoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("createSalida")]
        public async Task<IActionResult> CreateSalida([FromBody] RegistrarSalidaMedicamentoCommand command)
        {
            var create = await _mediator.Send(command);

            if (create != null)
            {
                return Ok(create);
            }
            return BadRequest();
        }
    }
}
