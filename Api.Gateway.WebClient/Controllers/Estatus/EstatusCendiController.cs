using Api.Gateway.Proxies.Estatus.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Estatus.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("estatus/cendis")]
    public class EstatusCendiController : ControllerBase
    {
        private readonly IQCTEstatusCendiProxy _estatus;

        public EstatusCendiController(IQCTEstatusCendiProxy estatus)
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
        [Route("getEstatusCendis/{modulo}/{submodulo}")]
        public async Task<IActionResult> GetEstatusCendis(int modulo, int submodulo)
        {
            var estatus = await _estatus.GetEstatusCendis(modulo, submodulo);

            return Ok(estatus);
        }
    }
}
