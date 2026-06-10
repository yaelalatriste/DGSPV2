using Api.Gateway.Proxies.SMedicos.Queries.Siacom.Consultorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.SMedicos.Siacom.Catalogos.Consultorios
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("smedicos/consultorios")]
    public class ConsultorioController : ControllerBase
    {
        private readonly IQCTConsultorioProxy _consultorios;

        public ConsultorioController(IQCTConsultorioProxy consultorios)
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
