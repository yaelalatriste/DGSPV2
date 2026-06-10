using DGSP.Module.Catalogos.Application.Services.SMedicos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Catalogos.Queries.SMedicos
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/catalogos/[controller]")]
    public class CTTiposMovimientoController : ControllerBase
    {
        private readonly ICTTipoMovimientoService _tipoMovimiento;

        public CTTiposMovimientoController(ICTTipoMovimientoService tipoMovimiento)
        {
            _tipoMovimiento = tipoMovimiento;
        }

        [HttpGet]
        [Route("getAllTiposMovimientos")]
        public async Task<IActionResult> GetAllTiposMovimientosAsync()
        {
            var movimientos = await _tipoMovimiento.GetAllTiposMovimientosAsync();

            return Ok(movimientos);
        }
        
        [HttpGet]
        [Route("getTipoMovimientoById/{id}")]
        public async Task<IActionResult> GetMovimientoById(int id)
        {
            var movimiento = await _tipoMovimiento.GetTipoMovimientoByIdAsync(id);

            return Ok(movimiento);
        }
        
        [HttpGet]
        [Route("getTiposMovimientosEntrada")]
        public async Task<IActionResult> GetMovimientosEntradaAsync()
        {
            var movimiento = await _tipoMovimiento.GetMovimientosEntradaAsync();

            return Ok(movimiento);
        }
        
        [HttpGet]
        [Route("getTiposMovimientosSalida")]
        public async Task<IActionResult> GetMovimientosSalida()
        {
            var movimiento = await _tipoMovimiento.GetMovimientosSalidaAsync();

            return Ok(movimiento);
        }
    }
}
