using Api.Gateway.Proxies.Seguros.Queries.Continuidades;
using Api.Gateway.Proxies.Usuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace Seguros.Api.Controllers.Continuidades.Queries
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("seguros/continuidades")]
    public class QCEntregableController : Controller
    {
        private readonly IQContinuidadProxy _continuidad;
        private readonly IUsuarioProxy _usuario;
        private readonly IHostingEnvironment _environment;

        public QCEntregableController(IQContinuidadProxy continuidad, IUsuarioProxy usuario, IHostingEnvironment environment)
        {
            _continuidad = continuidad;
            _usuario = usuario;
            _environment = environment;
        }

        [HttpGet]
        [Route("getEntregablesByContinuidad/{id}")]
        public async Task<IActionResult> GetEntregablesByContinuidad(int id)
        {
            var continuidad = await _continuidad.GetEntregablesByContinuidad(id);
            foreach (var e in continuidad)
            {
                e.Usuario = await _usuario.GetUsuarioByIdAsync(e.UsuarioId);
            }
            return Ok(continuidad);
        }

        [Route("visualizarEntregable/{expediente}/{tipo}/{archivo}")]
        [HttpGet]
        public async Task<string> VisualizarEntregable(int expediente, string tipo, string archivo)
        {
            var file = await _continuidad.VisualizarEntregable(expediente, tipo, archivo);

            return file.ToString();
        }
    }
}
