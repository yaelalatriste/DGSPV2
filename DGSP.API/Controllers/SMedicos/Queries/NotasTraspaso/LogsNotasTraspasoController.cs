using DGSP.Module.SMedicos.Application.Services.Medicamentos.Logs;
using DGSP.Module.SMedicos.Application.Services.Medicamentos.NotasTraspaso;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.SMedicos.Queries.NotasTraspaso
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/smedicos/[controller]")]
    public class LogsNotasTraspasoController : ControllerBase
    {
        private readonly ILogNotaTraspasoQueryService _logsNotaTraspasoQueryService;

        public LogsNotasTraspasoController(ILogNotaTraspasoQueryService logsNotaTraspasoQueryService)
        {
            _logsNotaTraspasoQueryService = logsNotaTraspasoQueryService;
        }
        
        [HttpGet("getLogsByNotaTraspaso/{notaId}")]
        public async Task<IActionResult> GetLogsByNotaTraspaso(int notaId)
        {
            var result = await _logsNotaTraspasoQueryService.GetLogsByNotaTraspasoAsync(notaId);

            return Ok(result);
        }
    }
}
