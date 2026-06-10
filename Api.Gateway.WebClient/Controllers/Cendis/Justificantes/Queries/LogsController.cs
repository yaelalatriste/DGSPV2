using System.Threading.Tasks;
using Api.Gateway.Proxies.Cendis.Queries.JUsuarios;
using Api.Gateway.Proxies.Cendis.Queries.Logs;
using Api.Gateway.Proxies.Estatus.Queries;
using Api.Gateway.Proxies.Usuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.WebClient.Controllers.Cendis.Justificantes.Queries
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("cendis/logs")]
    public class LogsController : ControllerBase
    {
        private readonly IQLJustificanteProxy _logJ;
        private readonly IUsuarioProxy _usuarios;
        private readonly IQCTEstatusCendiProxy _estatus;

        public LogsController(IQLJustificanteProxy logJ, IUsuarioProxy usuarios, IQCTEstatusCendiProxy estatus)
        {
            _logJ = logJ;
            _usuarios = usuarios;
            _estatus = estatus;
        }

        [HttpGet]
        [Route("justificantes/getAllLogsJustificantes")]
        public async Task<IActionResult> GetAllLogJustificantes()
        {
            var justificantes = await _logJ.GetLogJustificantesAsync();

            return Ok(justificantes);
        }


        [HttpGet]
        [Route("justificantes/getLogByJustificanteId/{id}")]
        public async Task<IActionResult> GetJustificanteById(int id)
        {
            var justificantes = await _logJ.GetLogByJustificanteId(id);

            foreach (var j in justificantes)
            { 
                j.Estatus = await _estatus.GetEstatusById(j.EstatusId);
                j.Usuario = await _usuarios.GetUsuarioByIdAsync(j.UsuarioId);
            }

            return Ok(justificantes);
        }
    }
}
