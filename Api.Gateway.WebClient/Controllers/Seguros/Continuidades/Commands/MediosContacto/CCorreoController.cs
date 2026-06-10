using Api.Gateway.Models.Seguros.Commands.Continuidades.MediosContacto;
using Api.Gateway.Proxies.Seguros.Commands.Continuidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Seguros.Continuidades.Commands.MediosContacto
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("seguros/continuidades")]
    public class CCorreoController:ControllerBase
    {
        private readonly ICContinuidadesProxy _continuidad;

        public CCorreoController(ICContinuidadesProxy continuidad)
        {
            _continuidad = continuidad;
        }

        [HttpPost]
        [Route("createCorreo")]
        public async Task<IActionResult> CreateCorreo(CorreoCreateCommand command)
        {
            var correo = await _continuidad.CreateCorreo(command);

            return Ok(correo);
        }
        
        [HttpPut]
        [Route("updateCorreo")]
        public async Task<IActionResult> UpdateCorreo(CorreoUpdateCommand command)
        {
            var correo = await _continuidad.UpdateCorreo(command);

            return Ok(correo);
        }
    }
}
