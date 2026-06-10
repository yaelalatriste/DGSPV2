using Api.Gateway.Models.Catalogos.DTOs.CTProcesos;
using Api.Gateway.Proxies.Catalogos.CTActividades.Queries;
using Api.Gateway.Proxies.Catalogos.CTPProcesos.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Catalogos.CTProcesos
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("catalogos/ctprocesos")]
    public class CTPProcesoController : ControllerBase
    {
        private readonly IQCTPProcesoProxy _cTPProceso;
        private readonly IQCTActividadProxy _actividades;

        public CTPProcesoController(IQCTPProcesoProxy cTPProceso, IQCTActividadProxy actividades)
        {
            _cTPProceso = cTPProceso;
            _actividades = actividades;
        }

        [HttpGet]
        public async Task<List<CTPProcesoDto>> GetPProcesosAsync()
        {
            var pprocesos = await _cTPProceso.GetPProcesosAsync();

            return pprocesos;
        }

        [HttpGet]
        [Route("getPProcesosByArea/{idArea}")]
        public async Task<List<CTPProcesoDto>> GetPProcesosByArea(int idArea)
        {
            var pprocesos = await _cTPProceso.GetPProcesosByArea(idArea);

            foreach (var pp in pprocesos)
            {
                if ((await _actividades.GetActividadesByProceso(pp.Id)).Count != 0)
                {
                    pp.Actividades = await _actividades.GetActividadesByProceso(pp.Id);
                }
            }

            return pprocesos;
        }
    }
}
