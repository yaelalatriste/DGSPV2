using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Entradas;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.SMedicos.Commands.Inventarios
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/smedicos/[controller]")]
    public class InventariosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventariosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createLote")]
        public async Task<IActionResult> RegistrarLote([FromBody] RegistrarLoteMedicamentoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
