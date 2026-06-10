using DGSP.Module.DGRH.Application.Queries.Empleado;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.DGRH.Queries
{
    [ApiController]
    [Route("api/dgrh/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoQueryService _emp;

        public EmpleadoController(IEmpleadoQueryService emp)
        {
            _emp = emp;
        }

        [HttpGet]
        [Route("getAllEmpleados")]
        public async Task<IActionResult> GetAllEmpleados()
         {
            var empleados = await _emp.GetAllEmpleados();

            return Ok(empleados);
        }
        
        [HttpGet]
        [Route("getEmpleadoByExpediente/{exp}")]
        public async Task<IActionResult> GetEmpleadoByExpediente(int exp)
         {
            var empleado = await _emp.GetEmpleado(exp);

            return Ok(empleado);
        }
        
        [HttpGet]
        [Route("getMovimientosEmpleado/{exp}")]
        public async Task<IActionResult> GetMovimientosEmpleado(int exp)
        {
            var empleado = await _emp.GetMovimientosEmpleado(exp);

            return Ok(empleado);
        }
       
        [HttpGet]
        [Route("getNivelesTE")]
        public async Task<IActionResult> GetNivelesTE()
        {
            var empleado = await _emp.GetEmpleadosTEAsync();

            return Ok(empleado);
        }
    }
}
