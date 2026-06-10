using Api.Gateway.Models.Usuarios;
using Api.Gateway.Models.Usuarios.DTOs;
using Api.Gateway.Proxies.Permisos;
using Api.Gateway.Proxies.Usuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Usuarios
{
    [ApiController]
    [Route("sausuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly ISAUsuarioProxy _usuarios;

        public UsuarioController(ISAUsuarioProxy usuarios)
        {
            _usuarios = usuarios;
        }
        
        [Route("getUsuarioByExpediente/{expediente}")]
        [HttpGet]
        public async Task<UserDto> GetUsuarioByExpediente(int expediente)
        {
            var usuarios = await _usuarios.GetUsuarioByExpediente(expediente);

            return usuarios;
        }
    }
}
