using Api.Gateway.Proxies.Seguros.Queries.Continuidades;
using Api.Gateway.Proxies.Usuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Seguros.Continuidades.Queries
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("seguros/continuidades/oficios")]
    public class QOficiosController : ControllerBase
    {
        private readonly IQOficiosContinuidadesProxy _qOficios;
        private readonly IUsuarioProxy _usuario;

        public QOficiosController(IQOficiosContinuidadesProxy qOficios, IUsuarioProxy usuario)
        {
            _qOficios = qOficios;
            _usuario = usuario;
        }

        [HttpGet]
        [Route("getOficiosContinuidades")]
        public async Task<IActionResult> GetOficiosContinuidades()
        {
            var oficios = await _qOficios.GetOficios();

            return Ok(oficios);
        }

        [HttpGet]
        [Route("getOficioById/{id}")]
        public async Task<IActionResult> GetOficioById(int id)
        {
            var oficio = await _qOficios.GetOficioById(id);
            oficio.Usuario = await _usuario.GetUsuarioByIdAsync(oficio.UsuarioId);
            return Ok(oficio);
        }

        [HttpGet]
        [Route("getContinuidadesByOficio/{id}")]
        public async Task<IActionResult> GetContinuidadesByOficio(int id)
        {
            var continuidad = await _qOficios.GetContinuidadesByOficio(id);

            return Ok(continuidad);
        }
    }
}
