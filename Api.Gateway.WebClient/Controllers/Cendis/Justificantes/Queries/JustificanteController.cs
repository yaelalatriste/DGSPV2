using Api.Gateway.Proxies.Catalogos.CTCendis.Queries;
using Api.Gateway.Proxies.Catalogos.CTIncidencias;
using Api.Gateway.Proxies.Catalogos.CTMeses.Queries;
using Api.Gateway.Proxies.Cendis.Queries;
using Api.Gateway.Proxies.Cendis.Queries.Justificantes;
using Api.Gateway.Proxies.Cendis.Queries.JUsuarios;
using Api.Gateway.Proxies.Estatus.Queries;
using Api.Gateway.Proxies.Usuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Cendis.Justificantes.Queries
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("cendis/justificantes")]
    public class JustificanteController : ControllerBase
    {
        private readonly IQJustificanteCendisProxy _justificantes;
        private readonly IQCTCendisProxy _ctCendis;
        private readonly IUsuarioProxy _usuarios;
        private readonly IQJUsuariosProxy _jusuarios;
        private readonly IQCTMesProxy _meses;
        private readonly IQCTIncidenciaProxy _ctIncidencias;
        private readonly IQCTEstatusCendiProxy _estatus;

        public JustificanteController(IQJustificanteCendisProxy justificantes, IQCTCendisProxy ctCendis, IUsuarioProxy usuarios, IQCTMesProxy meses, 
            IQCTIncidenciaProxy ctIncidencias, IQCTEstatusCendiProxy estatus, IQJUsuariosProxy jusuarios)
        {
            _justificantes = justificantes;
            _ctCendis = ctCendis;
            _usuarios = usuarios;
            _meses = meses;
            _ctIncidencias = ctIncidencias;
            _estatus = estatus;
            _jusuarios = jusuarios;
        }

        [HttpGet]
        [Route("getAlljustificantes")]
        public async Task<IActionResult> GetAllJustificantes()
        {
            var justificantes = await _justificantes.GetAllJustificantes();
            foreach (var j in justificantes) 
            {
                j.Cendi = await _ctCendis.GetCendisByIdAsync(j.CendiId);
                j.Mes = await _meses.GetMesById(j.MesId);
                j.Usuario = await _usuarios.GetUsuarioByIdAsync(j.UsuarioId);
                j.Estatus = await _estatus.GetEstatusById(j.EstatusId);
            }

            return Ok(justificantes);
        }

        [HttpGet]
        [Route("getJustificantesByCendi/{cendi}")]
        public async Task<IActionResult> GetJustificantesByCendi(int cendi)
        {
            var justificantes = await _justificantes.GetJustificantesByCendi(cendi);
            foreach (var j in justificantes)
            {
                j.Usuario = await _usuarios.GetUsuarioByIdAsync(j.UsuarioId);
                j.Estatus = await _estatus.GetEstatusById(j.EstatusId);
                j.Mes = await _meses.GetMesById(j.MesId);
            }
            return Ok(justificantes);
        }

        [HttpGet]
        [Route("getJustificanteById/{id}")]
        public async Task<IActionResult> GetJustificanteById(int id)
        {
            var justificante = await _justificantes.GetJustificanteById(id);
            justificante.Cendi = await _ctCendis.GetCendisByIdAsync(justificante.CendiId);
            justificante.Mes = await _meses.GetMesById(justificante.MesId);
            justificante.Usuario = await _usuarios.GetUsuarioByIdAsync(justificante.UsuarioId);
            justificante.Estatus = await _estatus.GetEstatusById(justificante.EstatusId);
            return Ok(justificante);
        }

        [HttpGet]
        [Route("getDetalleByJustificantes/{id}")]
        public async Task<IActionResult> GetDetalleByJustificantes(int id)
        {
            var detalle = await _justificantes.GetDetalleJustificante(id);

            foreach (var dt in detalle)
            {
                dt.Usuario = await _jusuarios.GetJUsuarioById(dt.UsuarioId);
                dt.Incidencia = await _ctIncidencias.GetCTIncidenciaById(dt.IncidenciaId);
            }

            return Ok(detalle);
        }
    }
}
