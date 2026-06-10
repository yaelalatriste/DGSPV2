using DGSP.Module.SMedicos.Application.Services.Medicamentos.Movimientos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.SMedicos.Queries.Movimientos
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/smedicos/[controller]")]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoInventarioService _movimientos;

        public MovimientosController(IMovimientoInventarioService movimientos)
        {
            _movimientos = movimientos;
        }
        
        [HttpGet("getAllMovimientos")]
        public async Task<IActionResult> GetAllMovimientosInventariosAsync()
        {
            var result = await _movimientos.GetAllMovimientosInventariosAsync();

            return Ok(result);
        }
        
        [HttpGet("getMovimientosByLote/{lote}")]
        public async Task<IActionResult> GetMovimientosByLote(int lote)
        {
            var result = await _movimientos.GetMovimientosInventariosByLoteAsync(lote);

            return Ok(result);
        }
    }
}
