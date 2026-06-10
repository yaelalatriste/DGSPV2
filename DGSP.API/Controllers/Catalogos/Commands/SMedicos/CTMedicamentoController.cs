using DGSP.Module.Catalogos.Application.Services.SMedicos;
using DGSP.Shared.Contracts.Commands.Catalogos.SMedicos.Medicamentos;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DGSP.API.Controllers.Catalogos.Commands.SMedicos
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/catalogos/medicamentos")]
    public class CTMedicamentoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CTMedicamentoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("createCTMedicamento")]
        public async Task<IActionResult> CreateMedicamento([FromBody] RegistrarCTMedicamentoCommand command)
        {
            var medicamento = await _mediator.Send(command);

            return Ok(medicamento);
        }

        [HttpPut]
        [Route("updateCTMedicamento")]
        public async Task<IActionResult> UpdateMedicamento([FromBody] ActualizarCTMedicamentoCommand command)
        {
            var medicamento = await _mediator.Send(command);

            return Ok(medicamento);
        }
    }
}
