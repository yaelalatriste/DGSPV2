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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("usuarios")]
    public class UserController : ControllerBase
    {
        private readonly IUsuarioProxy _usuarios;
        private readonly IPermisoProxy _permisos;

        public UserController(IUsuarioProxy usuarios, IPermisoProxy permisos)
        {
            _usuarios = usuarios;
            _permisos = permisos;
        }

        [HttpGet]
        [Route("getAllUsuarios")]
        public async Task<List<UsuarioDto>> GetAllUsuariosAsync()
        {
            var result = await _usuarios.GetAllUsuariosAsync();

            return result;
        }

        [Route("getUsuarioById/{usuario}")]
        [HttpGet]
        public async Task<UsuarioDto> GetUsuarioById(string usuario)
        {
            var usuarios = await _usuarios.GetUsuarioByIdAsync(usuario);
            usuarios.permisos = await _permisos.GetPermisosUsuarioAsync(usuario);

            return usuarios;
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
