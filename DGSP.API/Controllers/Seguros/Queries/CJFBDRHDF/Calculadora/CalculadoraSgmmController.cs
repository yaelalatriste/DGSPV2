using DGSP.Module.Seguros.Application.Services.CJFBDRHDF.Calculadora;
using DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Calculadora;
using DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Catalogos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Seguros.Queries.DGSP.Continuidades
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/seguros/[controller]")]
    public class CalculadoraSgmmController : ControllerBase
    {
        private readonly ICatalogosSgmmService _catalogos;
        private readonly ICalcularPolizaSgmmService _calculadoraSgmmService;

        public CalculadoraSgmmController(ICatalogosSgmmService catalogos, ICalcularPolizaSgmmService calculadoraSgmmService)
        {
            _catalogos = catalogos;
            _calculadoraSgmmService  = calculadoraSgmmService;
        }

        [HttpGet]
        [Route("getAllCatalogosSgmm")]
        public async Task<IActionResult> GetAllContinuidades()
        {
            var ct = new ObtenerCatalogosSgmmDto (2026);
            var continuidades = await _catalogos.ObtenerCatalogosSgmm(ct);

            return Ok(continuidades);
        }
        
        [HttpPost]
        [Route("calcular")]
        public async Task<IActionResult> Calcular([FromBody] FiltroSGMMDto calcular)
        {
            var continuidades = await _calculadoraSgmmService.ObtenerPrimasPotenciadasAsync(calcular);

            return Ok(continuidades);
        }
    }
}
