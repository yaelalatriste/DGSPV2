using DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.MediosContacto;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Seguros.Commands.Continuidades
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/seguros/continuidades/[controller]")]
    public class ContactoContinuidadController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactoContinuidadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("createContacto")]
        public async Task<IActionResult> CreateContacto([FromBody] RegistrarContactoContinuidadCommand command)
        {
            var Contacto = await _mediator.Send(command);

            return Ok(Contacto);
        }
        
        [HttpPut]
        [Route("updateContacto")]
        public async Task<IActionResult> UpdateContacto([FromBody] ActualizarContactoContinuidadCommand command)
        {
            var Contacto = await _mediator.Send(command);

            return Ok(Contacto);
        }
    }
}
