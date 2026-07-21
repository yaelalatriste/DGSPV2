using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Logs;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.SMedicos.Commands.NotasTraspaso
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/smedicos/[controller]")]
    public class LogsNotasTraspasoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LogsNotasTraspasoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("createLog")]
        public async Task<IActionResult> CreateLogNotaTraspaso([FromBody] RegistrarLogNotaTraspasoCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
