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
    public class SalidaMedicamentoDetalleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalidaMedicamentoDetalleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("createDetalleSalida")]
        public async Task<IActionResult> CreateDetalleSalida([FromBody] RegistrarSalidaMedicamentoDetalleCommand command)
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
