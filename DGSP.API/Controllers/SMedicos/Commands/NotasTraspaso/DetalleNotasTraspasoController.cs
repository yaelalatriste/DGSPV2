using DGSP.Module.SMedicos.Application.Services.Medicamentos.NotasTraspaso;
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
    public class DetalleNotasTraspasoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DetalleNotasTraspasoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createDetalleNotaTraspaso")]
        public async Task<IActionResult> GetDetallesNotaTraspasoAsync([FromBody] RegistrarDetalleNotaTraspasoCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
        
        [HttpPut("updateDetalleNotaTraspaso")]
        public async Task<IActionResult> UpdateDetallesNotaTraspasoAsync([FromBody] ActualizarDetalleNotaTraspasoCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
        
        [HttpPut("deleteDetalleNotaTraspaso")]
        public async Task<IActionResult> DeleteDetalleNotaTraspasoAsync([FromBody] EliminarDetalleNotaTraspasoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
