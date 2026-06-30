using DGSP.Module.Seguros.Application.Services.CJFBDRHDF.Calculadora;
using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
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
            calcular.Anio = 2026;
            calcular.TipoPoliza = 61;
            calcular.Quincenas = 13;
            calcular.SumaBasica= 1;
            calcular.IQ = 1;
            calcular.FechaInicio = Convert.ToDateTime("2025-12-31");
            calcular.FechaFinal = Convert.ToDateTime("2026-12-31");

            var continuidades = await _calculadoraSgmmService.CalcularPolizaSgmmAsync(calcular);

            return Ok(continuidades);
        }
    }
}
