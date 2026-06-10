using Api.Gateway.Proxies.Catalogos.CTMovimientos.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Catalogos.CTMovimientos
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("catalogos/movimientos")]
    public class CTMovimientoController : ControllerBase
    {
        private readonly IQCTMovimientoProxy _movimiento;

        public CTMovimientoController(IQCTMovimientoProxy movimiento)
        {
            _movimiento = movimiento;
        }

        [HttpGet]
        [Route("getAllMovimientos")]
        public async Task<IActionResult> GetAllMovimientos()
        {
            var movimientos = await _movimiento.GetAllMovimientos();

            return Ok(movimientos);
        }

        [HttpGet]
        [Route("getMovimientoById")]
        public async Task<IActionResult> GetMovimientoById(int id)
        {
            var movimiento = await _movimiento.GetMovimientoById(id);

            return Ok(movimiento);
        }
    }
}
