using Api.Gateway.Models.Seguros.Commands.Continuidades.Continuidad;
using Api.Gateway.Proxies.Cendis.Queries.JUsuarios;
using Api.Gateway.Proxies.DGRH.Queries.Empleado;
using Api.Gateway.Proxies.Estatus.Queries.Continuidades;
using Api.Gateway.Proxies.Seguros.Queries.Continuidades;
using Api.Gateway.Proxies.Usuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Seguros.Continuidades.Queries
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("seguros/continuidades")]
    public class QContinuidadesController : ControllerBase
    {
        private readonly IQContinuidadProxy _continuidad;
        private readonly IQEmpleadoProxy _empleado;
        private readonly IQEContinuidadesProxy _estatus;
        private readonly IUsuarioProxy _usuario;

        public QContinuidadesController(IQContinuidadProxy continuidad, IQEmpleadoProxy empleado, IQEContinuidadesProxy estatus, IUsuarioProxy usuario)
        {
            _continuidad = continuidad;
            _empleado = empleado;
            _estatus = estatus;
            _usuario = usuario;
        }

        [HttpGet]
        [Route("getAllContinuidades")]
        public async Task<IActionResult> GetAllContinuidades()
        {
            var continuidades = await _continuidad.GetAllContinuidades();

            return Ok(continuidades);
        }
        
        [HttpPut]
        [Route("actualizarContinuidad")]
        public async Task<IActionResult> ActualizarContinuidad([FromBody] ContinuidadUpdateCommand command)
        {
            var continuidades = await _continuidad.ActualizarContinuidad(command);

            return Ok(continuidades);
        }
        
        [HttpGet]
        [Route("vgetContinuidadesByEstatus/{estatus}")]
        public async Task<IActionResult> VGetContinuidadesByEstatus(int estatus)
        {
            var continuidades = await _continuidad.VGetContinuidadesByEstatus(estatus);

            return Ok(continuidades);
        }

        [HttpGet]
        [Route("getContinuidad/{exp}")]
        public async Task<IActionResult> GetContinuidad(int exp)
        {
            var continuidad = await _continuidad.GetContinuidad(exp);
            continuidad.Empleado = await _empleado.GetEmpleadoByExpediente(exp);
            continuidad.Estatus = await _estatus.GetEstatusById((int)continuidad.EstatusId);
            continuidad.Entregables = await _continuidad.GetEntregablesByContinuidad(continuidad.Id);
            continuidad.Correos = await _continuidad.GetCorreosByContinuidad(continuidad.Id);
            foreach (var e in continuidad.Entregables)
            {
                e.Usuario = await _usuario.GetUsuarioByIdAsync(e.UsuarioId);
            }
            return Ok(continuidad);
        }

        [HttpGet]
        [Route("getContinuidadById/{id}")]
        public async Task<IActionResult> getContinuidadById(int id)
        {
            var continuidad = await _continuidad.GetContinuidadById(id);
            continuidad.Empleado = await _empleado.GetEmpleadoByExpediente(continuidad.Expediente);
            continuidad.Estatus = await _estatus.GetEstatusById((int)continuidad.EstatusId);
            continuidad.Entregables = await _continuidad.GetEntregablesByContinuidad(continuidad.Id);
            continuidad.Correos = await _continuidad.GetCorreosByContinuidad(continuidad.Id);
            foreach (var e in continuidad.Entregables)
            {
                e.Usuario = await _usuario.GetUsuarioByIdAsync(e.UsuarioId);
            }
            return Ok(continuidad);
        }

        [HttpGet]
        [Route("getContinuidadesByEstatus/{estatus}")]
        public async Task<IActionResult> getContinuidadesByEstatus(int estatus)
        {
            var continuidad = await _continuidad.GetContinuidadesByEstatus(estatus);
            foreach (var e in continuidad)
            {
                e.Usuario = await _usuario.GetUsuarioByIdAsync(e.UsuarioId);
                e.Empleado = await _empleado.GetEmpleadoByExpediente(e.Expediente);
                e.Estatus = await _estatus.GetEstatusById((int)e.EstatusId);
            }
            return Ok(continuidad);
        }
    }
}
