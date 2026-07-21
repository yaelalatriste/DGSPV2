using DGSP.Module.Estatus.Application.Services.Continuidades;
using DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.Continuidad;
using DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Logs;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Seguros.Commands.Continuidades
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/seguros/continuidades/[controller]")]
    public class ContinuidadController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEstatusContinuidadesService _estatusContinuidad;

        public ContinuidadController(IMediator mediator, IEstatusContinuidadesService estatusContinuidad)
        {
            _mediator = mediator;
            _estatusContinuidad = estatusContinuidad;
        }

        [HttpPost]
        [Route("createContinuidad")]
        public async Task<IActionResult> CreateContinuidad([FromBody] RegistrarContinuidadCommand command)
        {
            var Continuidad = await _mediator.Send(command);

            return Ok(Continuidad);
        }
        
        [HttpPut]
        [Route("updateContinuidad")]
        public async Task<IActionResult> UpdateContinuidad([FromBody] ActualizarContinuidadCommand command)
        {
            var Continuidad = await _mediator.Send(command);

            return Ok(Continuidad);
        }
        
        [HttpPut]
        [Route("estatusContinuidad")]
        public async Task<IActionResult> ActualizarEstatusContinuidadAsync([FromBody] EstatusContinuidadCommand command)
        {
            var continuidad = await _mediator.Send(command);
            var estatus = await _estatusContinuidad.GetEstatusById(continuidad.EstatusId);

            if (continuidad != null)
            {
                RegistrarLogContinuidadCommand log = new RegistrarLogContinuidadCommand();
                log.ContinuidadId = command.Id;
                log.UsuarioId = command.UsuarioId;
                log.EstatusId = command.EstatusId;
                log.Observaciones = (command.Corregir ? command.Observaciones+" (Se regresa al estatus <b>"+estatus.Nombre+"</b>, ya que se realizó un movimiento de corrección)" : command.Observaciones);
                await _mediator.Send(log);
            }
            return Ok(continuidad);
        }
    }
}
