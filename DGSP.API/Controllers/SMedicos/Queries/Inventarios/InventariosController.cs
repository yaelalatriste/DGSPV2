using DGSP.Module.SMedicos.Application.Services.Medicamentos.Entradas;
using DGSP.Module.SMedicos.Application.Services.Medicamentos.Movimientos;
using DGSP.Module.SMedicos.Persistence.Services.Medicamentos.Movimientos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.SMedicos.Queries.Inventarios
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/smedicos/[controller]")]
    public class InventariosController : ControllerBase
    {
        private readonly IInventarioAppService _inventarioQueryService;

        public InventariosController(IInventarioAppService inventarioQueryService)
        {
            _inventarioQueryService = inventarioQueryService;
        }

        [HttpGet("lotes")]
        public async Task<IActionResult> GetLotes()
        {
            var result = await _inventarioQueryService.ConsultarLotesAsync();

            return Ok(result);
        }
        
        [HttpGet("getLoteById/{id}")]
        public async Task<IActionResult> GetLoteById(int id)
        {
            var result = await _inventarioQueryService.GetLoteByIdAsync(id);

            return Ok(result);
        }
       
        [HttpGet("getDatosByLote/{lote}")]
        public async Task<IActionResult> GetDatosByLoteAsync(string lote)
        {
            var result = await _inventarioQueryService.GetDatosByLoteAsync(lote);

            return Ok(result);
        }        
        
        [HttpGet("getMedicamentosByLoteConsultorio/{lote}/{consultorio}")]
        public async Task<IActionResult> GetMedicamentosByLoteConsultorio(string lote, int consultorio)
        {
            var result = await _inventarioQueryService.GetMedicamentosByLoteConsultorioAsync(lote, consultorio);

            return Ok(result);
        }        
       
        [HttpGet("getDatosByLoteConsultorio/{lote}/{consultorio}")]
        public async Task<IActionResult> GetDatosByLoteConsultorioAsync(string lote, int consultorio)
        {
            var result = await _inventarioQueryService.GetDatosByLoteConsultorioAsync(lote, consultorio);

            return Ok(result);
        }        
        
        [HttpGet("getDatosByLoteConsultorioMedicamento/{lote}/{consultorio}/{medicamento}")]
        public async Task<IActionResult> GetDatosByLoteConsultorioAsync(string lote, int consultorio, int medicamento)
        {
            var result = await _inventarioQueryService.GetDatosByLoteConsultorioMedicamentoAsync(lote, consultorio, medicamento);

            return Ok(result);
        }        
    }
}
