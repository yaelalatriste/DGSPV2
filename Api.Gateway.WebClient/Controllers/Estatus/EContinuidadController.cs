using Api.Gateway.Proxies.Estatus.Queries;
using Api.Gateway.Proxies.Estatus.Queries.Continuidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Estatus.Api.Controllers.Estatus
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("estatus/continuidades")]
    public class EstatusCendiController : ControllerBase
    {
        private readonly IQEContinuidadesProxy _estatus;

        public EstatusCendiController(IQEContinuidadesProxy estatus)
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
        public async Task<IActionResult> GetEstatusCendis(int id)
        {
            var estatus = await _estatus.GetEstatusById(id);

            return Ok(estatus);
        }
    }
}
