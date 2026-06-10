using DGSP.Module.SMedicos.Application.Interfaces.Siacom.Consultorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMedicos.Services.Queries.Queries.Siacom.Consultorios;

namespace DGSP.API.Controllers.SMedicos.Queries.SIACOM
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/smedicos/[controller]")]
    public class ConsultorioController : ControllerBase
    {
        private readonly ICTConsultorioQueryService _consultorios;

        public ConsultorioController(ICTConsultorioQueryService consultorios)
        {
            _consultorios = consultorios;
        }

        [HttpGet]
        [Route("getAllConsultorios")]
        public async Task<IActionResult> GetAllConsultorios()
        {
            var consultorios = await _consultorios.GetAllConsultorios();

            return Ok(consultorios);
        }

        [HttpGet]
        [Route("getConsultorioById/{id}")]
        public async Task<IActionResult> GetConsultorioById(int id)
        {
            var consultorios = await _consultorios.GetConsultorioById(id);

            return Ok(consultorios);
        }
    }
}
