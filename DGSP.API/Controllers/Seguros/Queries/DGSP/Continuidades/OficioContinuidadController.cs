using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Seguros.Queries.DGSP.Continuidades
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/seguros/continuidades/[controller]")]
    public class OficioContinuidadController : ControllerBase
    {
        private readonly IOficioContinuidadService _oficioService;

        public OficioContinuidadController(IOficioContinuidadService oficioService)
        {
            _oficioService = oficioService;
        }

        [HttpGet]
        [Route("getOficioByContinuidad/{continuidadId}")]
        public async Task<IActionResult> GetOficioByContinuidad(int continuidadId)
        {
            var continuidad = await _oficioService.GetOficiosByContinuidadAsync(continuidadId);

            return Ok(continuidad);
        }
    }
}
