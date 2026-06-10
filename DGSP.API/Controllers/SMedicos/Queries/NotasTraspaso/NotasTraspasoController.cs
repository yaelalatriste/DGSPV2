using DGSP.Module.SMedicos.Application.Services.Medicamentos.NotasTraspaso;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace DGSP.API.Controllers.SMedicos.Queries.NotasTraspaso
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/smedicos/[controller]")]
    public class NotasTraspasoController : ControllerBase
    {
        private readonly INotaTraspasoQueryService _notaTraspasoQueryService;
        private readonly IWebHostEnvironment _environment;

        public NotasTraspasoController(INotaTraspasoQueryService notaTraspasoQueryService, IWebHostEnvironment environment)
        {
            _notaTraspasoQueryService = notaTraspasoQueryService;
            _environment = environment;
        }

        [HttpGet("getAllNotasTraspaso")]
        public async Task<IActionResult> GetAllNotasTraspasoAsync()
        {
            var result = await _notaTraspasoQueryService.GetAllNotasTraspasoAsync();

            return Ok(result);
        }
        
        [HttpGet("getNotaTraspasoById/{id}")]
        public async Task<IActionResult> GetNotaTraspasoByIdAsync(int id)
        {
            var result = await _notaTraspasoQueryService.GetNotaTraspasoByIdAsync(id);

            return Ok(result);
        }

        [HttpGet("visualizarEntregable/{notaId}")]
        public async Task<string> VisualizarEntregable(int notaId)
        {
            var nota = await _notaTraspasoQueryService.GetNotaTraspasoByIdAsync(notaId);

            string folderPath = Path.Combine(_environment.ContentRootPath,"Entregables","SMedicos","NotasTraspaso","Memorandum",nota.NumeroTraspaso.ToString());

            string pathArchivo = Path.Combine(folderPath, nota.Entregable);

            return pathArchivo;
        }
    }
}
