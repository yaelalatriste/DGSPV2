using Api.Gateway.Models.Cendis.Commands.JUsuarios;
using Api.Gateway.Proxies.Cendis.Commands.JUsuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Cendis.JUsuarios.Commands
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("cendis/jusuarios")]
    public class CJUsuariosController : Controller
    {
        private readonly ICJUsuarioProxy _jusuarios;

        public CJUsuariosController(ICJUsuarioProxy jusuarios)
        {
            _jusuarios = jusuarios;
        }

        [Route("createJUsuario")]
        [HttpPost]
        public async Task<IActionResult> CreateJUsuario([FromBody] JUsuarioCreateCommand command)
        {
            var jusuarios = await _jusuarios.CreateJUsuario(command);
            if (jusuarios != null)
            {
                return Ok(jusuarios);
            }

            return BadRequest();
        }

        [Route("updateJUsuario")]
        [HttpPut]
        public async Task<IActionResult> UpdateJustificante([FromBody] JUsuarioUpdateCommand command)
        {
            var jusuarios = await _jusuarios.UpdateJUsuario(command);
            if (jusuarios != null)
            {
                return Ok(jusuarios);
            }

            return BadRequest();
        }
    }
}
