using Api.Gateway.Proxies.Catalogos.CTCendis.Queries;
using Api.Gateway.Proxies.Cendis.Queries.CendisUsuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Cendis.CendisUsuarios.Queries
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("cendis/cendisUsuarios")]
    public class QCendisUsuariosController : ControllerBase
    {
        private readonly IQCendisUsuariosProxy _cUsuarios;
        private readonly IQCTCendisProxy _ctCendis;

        public QCendisUsuariosController(IQCendisUsuariosProxy cUsuarios, IQCTCendisProxy ctCendis)
        {
            _cUsuarios = cUsuarios;
            _ctCendis = ctCendis;
        }

        [HttpGet]
        [Route("getAllCendisUsuarios")]
        public async Task<IActionResult> GetAllCendisUsuarios()
        {
            var cendis = await _cUsuarios.GetAllCendisUsuarios();
            foreach (var cn in cendis)
            {
                cn.Cendi = await _ctCendis.GetCendisByIdAsync(cn.CendiId);
            }

            return Ok(cendis);
        }


        [HttpGet]
        [Route("getCendisByusuario/{usuario}")]
        public async Task<IActionResult> GetJustificanteById(string usuario)
        {
            var cendis = await _cUsuarios.GetCendisByUsuario(usuario);
            foreach (var cn in cendis)
            {
                cn.Cendi = await _ctCendis.GetCendisByIdAsync(cn.CendiId);
            }

            return Ok(cendis);
        }

        [HttpGet]
        [Route("getUsuariosByCendi/{id}")]
        public async Task<IActionResult> GetUsuariosByCendi(int id)
        {
            var cendis = await _cUsuarios.GetUsuariosByCendi(id);
            foreach (var cn in cendis)
            {
                cn.Cendi = await _ctCendis.GetCendisByIdAsync(cn.CendiId);
            }

            return Ok(cendis);
        }
    }
}
