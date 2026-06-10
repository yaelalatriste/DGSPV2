using Api.Gateway.Proxies.Seguros.Queries.Reportes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Seguros.Reportes
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("seguros/reportes/jubilados")]
    public class RJubiladoController : ControllerBase
    {
        private readonly IRJubiladosProxy _rjubilados;

        public RJubiladoController(IRJubiladosProxy rjubilados)
        {
            _rjubilados = rjubilados;
        }

        [HttpGet]
        [Route("getRJubiladosFemenino")]
        public async Task<IActionResult> GetAllJubiladosFemeninoAsync()
        {
            var reporte = await _rjubilados.GetReporteFemenino();

            return Ok(reporte);
        }

        [HttpGet]
        [Route("getRJubiladosMasculino")]
        public async Task<IActionResult> GetAllJubiladosMasculinoAsync()
        {
            var reporte = await _rjubilados.GetReporteMasculino();

            return Ok(reporte);
        }
    }
}
