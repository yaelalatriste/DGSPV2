using DGSP.Module.SMedicos.Application.Services.Medicamentos.NotasTraspaso;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.SMedicos.Queries.NotasTraspaso
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/smedicos/[controller]")]
    public class DetalleNotasTraspasoController : ControllerBase
    {
        private readonly IDetalleNotaTraspasoQueryService _detalleNotaTraspasoQueryService;

        public DetalleNotasTraspasoController(IDetalleNotaTraspasoQueryService detalleNotaTraspasoQueryService)
        {
            _detalleNotaTraspasoQueryService = detalleNotaTraspasoQueryService;
        }

        [HttpGet("getDetallesNotaTraspasoByNota/{nota}")]
        public async Task<IActionResult> GetDetallesNotaTraspasoByNotaAsync(int nota)
        {
            var result = await _detalleNotaTraspasoQueryService.GetDetallesNotaTraspasoByNotaAsync(nota);

            return Ok(result);
        }
        
        [HttpGet("getDetalleNotaTraspasoById/{id}")]
        public async Task<IActionResult> GetDetalleNotaTraspasoByIdAsync(int id)
        {
            var result = await _detalleNotaTraspasoQueryService.GetDetalleNotaTraspasoByIdAsync(id);

            return Ok(result);
        }
    }
}
