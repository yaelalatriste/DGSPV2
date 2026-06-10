using DGSP.Module.SMedicos.Application.Services.Medicamentos.Salidas;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.SMedicos.Queries.Salidas
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/smedicos/[controller]")]
    public class SalidaMedicamentoController : ControllerBase
    {
        private readonly ISalidaMedicamentoService _salidaMedicamentoService;

        public SalidaMedicamentoController(ISalidaMedicamentoService salidaMedicamentoService)
        {
            _salidaMedicamentoService = salidaMedicamentoService;
        }

        [HttpGet]
        [Route("getAllSalidas")]
        public async Task<IActionResult> GetAllSalidasAsync()
        {
            var salidas = await _salidaMedicamentoService.GetAllSalidasAsync();

            return Ok(salidas);
        }
        
        [HttpGet]
        [Route("getSalidaById/{id}")]
        public async Task<IActionResult> GetSalidaByIdAsync(int id)
        {
            var salida = await _salidaMedicamentoService.ObtenerPorIdAsync(id);

            return Ok(salida);
        }
    }
}
