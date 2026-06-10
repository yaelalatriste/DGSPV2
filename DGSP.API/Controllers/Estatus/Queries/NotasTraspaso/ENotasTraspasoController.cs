using DGSP.Module.Estatus.Application.Services.NotasTraspaso;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Estatus.Queries.NotasTraspaso
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/estatus/[controller]")]
    public class ENotasTraspasoController : ControllerBase
    {
        private readonly IEstatusNotasTraspasoService  _estatus;

        public ENotasTraspasoController(IEstatusNotasTraspasoService estatus)
        {
            _estatus = estatus;
        }

        [HttpGet]
        [Route("getAllEstatus")]
        public async Task<IActionResult> GetAllEstatus()
        {
            var estatus = await _estatus.GetAllEstatus();

            return Ok(estatus);
        }

        [HttpGet]
        [Route("getEstatusById/{id}")]
        public async Task<IActionResult> GetEstatusById(int id)
        {
            var estatus = await _estatus.GetEstatusById(id);

            return Ok(estatus);
        }
    }
}
