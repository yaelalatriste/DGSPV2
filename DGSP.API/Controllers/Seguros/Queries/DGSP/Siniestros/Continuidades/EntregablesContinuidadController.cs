using DGSP.Module.Catalogos.Application.Services.Generales;
using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Seguros.Queries.DGSP.Siniestros.Continuidades
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/seguros/continuidades/[controller]")]
    public class EntregablesContinuidadController : ControllerBase
    {
        private readonly IContinuidadService _continuidadService;
        private readonly IEntregableContinuidadService _entregableService;
        private readonly ICTEntregableService _ctEntregableService;
        private readonly IWebHostEnvironment _environment;


        public EntregablesContinuidadController(IContinuidadService continuidadService, IEntregableContinuidadService entregableService, 
            ICTEntregableService ctEntregableService, IWebHostEnvironment environment)
        {
            _entregableService = entregableService;
            _ctEntregableService = ctEntregableService;
            _environment = environment;
            _continuidadService = continuidadService;
        }

        [HttpGet]
        [Route("getEntregablesByContinuidad/{continuidadId}")]
        public async Task<IActionResult> GetEntregablesByContinuidad(int continuidadId)
        {
            var continuidad = await _entregableService.GetEntregablesByContinuidad(continuidadId);

            return Ok(continuidad);
        }

        [HttpGet("visualizarEntregable/{entregableId}")]
        public async Task<string> VisualizarEntregable(int entregableId)
        {
            var entregable = await _entregableService.GetEntregableById(entregableId);
            var tipoEntregable = await _ctEntregableService.GetEntregableByIdAsync(entregable.EntregableId);
            var continuidad = await _continuidadService.GetContinuidadByIdAsync(entregable.ContinuidadId);

            string folderPath = Path.Combine(_environment.ContentRootPath, "Entregables", "Seguros", "Continuidades", continuidad.Expediente.ToString(), tipoEntregable.Abreviacion);

            string pathArchivo = Path.Combine(folderPath, entregable.Archivo);

            return pathArchivo;
        }
    }
}
