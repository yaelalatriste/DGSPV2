using Microsoft.AspNetCore.Authentication.JwtBearer;
using Api.Gateway.Proxies.Cendis.Queries.JUsuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Cendis.JUsuarios.Queries
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("cendis/jusuarios")]
    public class QJUsuariosController : ControllerBase
    {
        private readonly IQJUsuariosProxy _jusuarios;

        public QJUsuariosController(IQJUsuariosProxy jusuarios)
        {
            _jusuarios = jusuarios;
        }

        [HttpGet]
        [Route("getAllJUsuarios")]
        public async Task<IActionResult> GetAllJUsuarios()
        {
            var justificantes = await _jusuarios.GetAllJUsuarios();

            return Ok(justificantes);
        }


        [HttpGet]
        [Route("getJUsuarioById/{id}")]
        public async Task<IActionResult> GetJUsuarioById(int id)
        {
            var justificantes = await _jusuarios.GetJUsuarioById(id);

            return Ok(justificantes);
        }
        
        [HttpGet]
        [Route("getJUsuarioByExpediente/{id}")]
        public async Task<IActionResult> GetJUsuarioByExpediente(int id)
        {
            var justificantes = await _jusuarios.GetJUsuarioByExpediente(id);

            return Ok(justificantes);
        }
    }
}
