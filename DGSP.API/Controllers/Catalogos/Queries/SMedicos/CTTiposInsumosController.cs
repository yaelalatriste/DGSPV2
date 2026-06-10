using DGSP.Module.Catalogos.Application.Services.SMedicos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Catalogos.Queries.SMedicos
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/catalogos/[controller]")]
    public class CTTiposInsumoController : ControllerBase
    {
        private readonly ICTTipoInsumoService _tipoInsumo;

        public CTTiposInsumoController(ICTTipoInsumoService tipoInsumo)
        {
            _tipoInsumo = tipoInsumo;
        }

        [HttpGet]
        [Route("getAllTipoInsumos")]
        public async Task<IActionResult> GetAllTipoInsumosAsync()
        {
            var movimientos = await _tipoInsumo.GetAllTiposInsumosAsync();

            return Ok(movimientos);
        }
        
        [HttpGet]
        [Route("getTipoInsumoById/{id}")]
        public async Task<IActionResult> GetMovimientoById(int id)
        {
            var movimiento = await _tipoInsumo.GetTipoInsumoByIdAsync(id);

            return Ok(movimiento);
        }
    }
}
