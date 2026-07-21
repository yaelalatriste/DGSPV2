using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.Entregables;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Seguros.Commands.Continuidades
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/seguros/continuidades/[controller]")]
    public class EntregablesContinuidadController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EntregablesContinuidadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("createEntregable")]
        public async Task<IActionResult> CreateEntregable([FromForm] RegistrarEntregableContinuidadCommand command)
        {
            var entregable = await _mediator.Send(command);

            return Ok(entregable);
        }
        
        [HttpPut]
        [Route("updateEntregableContinuidad")]
        public async Task<IActionResult> UpdateEntregable([FromForm] ActualizarEntregableContinuidadCommand command)
        {
            var entregable = await _mediator.Send(command);

            return Ok(entregable);
        }

        [HttpPut]
        [Route("deleteEntregable")]
        public async Task<IActionResult> DeleteEntregable([FromForm] EliminarEntregableContinuidadCommand command)
        {
            var entregable = await _mediator.Send(command);

            return Ok(entregable);
        }
    }
}
