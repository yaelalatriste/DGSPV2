using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.SMedicos.Commands.NotasTraspaso
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/smedicos/[controller]")]
    public class NotasTraspasoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotasTraspasoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createNota")]
        public async Task<IActionResult> RegistrarNota([FromBody] RegistrarNotaTraspasoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        
        [HttpPut("updateNota")]
        public async Task<IActionResult> ActualizarNota([FromBody] ActualizarNotaTraspasoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        
        [HttpPut("concluirNota")]
        public async Task<IActionResult> ConcluirNota([FromForm] ConcluirNotaTraspasoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
