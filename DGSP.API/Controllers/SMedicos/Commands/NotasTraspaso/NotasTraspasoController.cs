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
    public class NotasTraspasoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDetalleNotaTraspasoQueryService _detalleNotaTraspasoQueryService;

        public NotasTraspasoController(IMediator mediator, IDetalleNotaTraspasoQueryService detalleNotaTraspasoQueryService)
        {
            _mediator = mediator;
            _detalleNotaTraspasoQueryService = detalleNotaTraspasoQueryService;
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
        
        [HttpPut("deleteNota")]
        public async Task<IActionResult> EliminarNota([FromBody] EliminarNotaTraspasoCommand command)
        {
            var result = await _mediator.Send(command);
            if (result != null)
            {
                var detalles = await _detalleNotaTraspasoQueryService.GetDetallesNotaTraspasoByNotaAsync(command.Id);

                foreach ( var dt in detalles) 
                { 
                    EliminarDetalleNotaTraspasoCommand eliminarDetalleCommand = new EliminarDetalleNotaTraspasoCommand
                    {
                        Id = dt.Id,
                        UsuarioId = command.UsuarioId,
                    };

                    await _mediator.Send(eliminarDetalleCommand);
                }
            }
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
