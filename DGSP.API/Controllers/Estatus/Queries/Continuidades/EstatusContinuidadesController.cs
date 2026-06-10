using DGSP.Module.Estatus.Application.Services.Continuidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Estatus.Queries.Continuidades
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/estatus/[controller]")]
    public class EstatusContinuidadesController : ControllerBase
    {
        private readonly IEstatusContinuidadesService _estatusContinuidades;

        public EstatusContinuidadesController(IEstatusContinuidadesService estatusContinuidades)
        {
            _estatusContinuidades = estatusContinuidades;
        }

        [HttpGet]
        [Route("getAllEstatus")]
        public async Task<IActionResult> GetAllEstatus()
        {
            var estatus = await _estatusContinuidades.GetAllEstatus();

            return Ok(estatus);
        }

        [HttpGet]
        [Route("getEstatusById/{id}")]
        public async Task<IActionResult> GetEstatusById(int id)
        {
            var estatus = await _estatusContinuidades.GetEstatusById(id);

            return Ok(estatus);
        }
    }
}
