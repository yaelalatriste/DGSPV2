using DGSP.Module.DGRH.Application.Services.RH;
using DGSP.Module.DGRH.Persistence.Services.RH.Empleados;
using DGSP.Shared.Contracts.DTOs.DGRH.RH.Empleados;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.DGRH.Queries
{
    [ApiController]
    [Route("api/dgrh/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;


        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet]
        [Route("getAllEmpleados")]
        public async Task<IActionResult> GetAllEmpleados()
         {
            var empleados = await _empleadoService.GetAllEmpleados();

            return Ok(empleados);
        }
        
        [HttpGet]
        [Route("getEmpleadoByExpediente/{exp}")]
        public async Task<IActionResult> GetEmpleadoByExpediente(int exp)
         {
            var empleado = await _empleadoService.GetEmpleado(exp);

            return Ok(empleado);
        }
        
        [HttpGet]
        [Route("getMovimientosEmpleado/{exp}")]
        public async Task<IActionResult> GetMovimientosEmpleado(int exp)
        {
            var empleado = await _empleadoService.GetMovimientosEmpleado(exp);

            return Ok(empleado);
        }
       
        [HttpGet]
        [Route("getNivelesTE")]
        public async Task<IActionResult> GetNivelesTE()
        {
            var empleado = await _empleadoService.GetEmpleadosTEAsync();

            return Ok(empleado);
        }

        [HttpPost("ultimos-puestos")]
        public async Task<ActionResult<List<UltimoPuestoEmpleadoDto>>> GetUltimosPuestos([FromBody] UltimosPuestosRequestDto request, CancellationToken cancellationToken)
        {
            if (request.Expedientes is null || request.Expedientes.Count == 0)
            {
                return Ok(Array.Empty<UltimoPuestoEmpleadoDto>());
            }

            var resultado = await _empleadoService.GetUltimosPuestosAsync(request.Expedientes, cancellationToken);

            return Ok(resultado);
        }
    }
}
