using Api.Gateway.Models.Seguros.Commands.Continuidades.OficiosContinuidades;
using Api.Gateway.Proxies.Seguros.Commands.Continuidades;
using Api.Gateway.Proxies.Seguros.Commands.Continuidades.OficiosContinuidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Seguros.Continuidades.Commands.OficiosContinuidades
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("seguros/continuidades/oficios")]
    public class COficioController:ControllerBase
    {
        private readonly ICOContinuidadesProxy _ocontinuidad;

        public COficioController(ICOContinuidadesProxy ocontinuidad)
        {
            _ocontinuidad = ocontinuidad;
        }

        [HttpPost]
        [Route("createOficio")]
        public async Task<IActionResult> CreateCorreo([FromBody] OContinuidadCreateCommand command)
        {
            var correo = await _ocontinuidad.CreateOficio(command);

            return Ok(correo);
        }
        
        [HttpPut]
        [Route("updateOficio")]
        public async Task<IActionResult> UpdateCorreo([FromBody] OContinuidadUpdateCommand command)
        {
            var correo = await _ocontinuidad.UpdateOficio(command);

            return Ok(correo);
        }

        [HttpPost]
        [Route("createContinuidadesByOficio")]
        public async Task<IActionResult> CreateContinuidadesByOficio([FromBody] List<COficioCreateCommand> command)
        {
            var correo = await _ocontinuidad.CreateContinuidadesByOficio(command);

            return Ok(correo);
        }
    }
}
