using Api.Gateway.Models.DG.Queries.Acuerdos;
using Api.Gateway.Proxies.Catalogos.CTAreas.Queries;
using Api.Gateway.Proxies.Catalogos.CTMeses.Queries;
using Api.Gateway.Proxies.Cendis.Queries.CendisUsuarios;
using Api.Gateway.Proxies.Cendis.Queries.JUsuarios;
using Api.Gateway.Proxies.DG.Queries.Acuerdos;
using Api.Gateway.Proxies.Estatus.Queries.Acuerdos;
using Api.Gateway.Proxies.Usuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.DG.Acuerdos.Queries
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("dg/acuerdos")]
    public class QAcuerdoController : ControllerBase
    {
        private readonly IQAcuerdoProxy _acuerdos;
        private readonly IUsuarioProxy _usuarios;
        private readonly IQCTMesProxy  _meses;
        private readonly IQEAcuerdoProxy _estatus;
        private readonly IQCTAreaProxy _areas;

        public QAcuerdoController(IQAcuerdoProxy acuerdos, IUsuarioProxy usuarios, IQEAcuerdoProxy estatus, IQCTAreaProxy areas, IQCTMesProxy meses)
        {
            _acuerdos = acuerdos;
            _usuarios = usuarios;
            _estatus = estatus;
            _areas = areas;  
            _meses = meses;
        }

        [Route("getAllAcuerdos")]
        [HttpGet]
        public async Task<List<AcuerdoDto>> GetAllAcuerdos()
        {
            var acuerdos = await _acuerdos.GetAllAcuerdosAsync();
            foreach (var ac in acuerdos)
            {
                ac.Elaboro = await _usuarios.GetUsuarioByIdAsync(ac.ElaboroId);
                ac.Estatus = await _estatus.GetEstatusById(ac.EstatusId);
                ac.Area = await _areas.GetAreaByIdAsync(ac.AreaId);
                ac.Mes = await _meses.GetMesById(ac.MesId);
            }

            return acuerdos;
        }

        [Route("getAcuerdoById/{id}")]
        [HttpGet]
        public async Task<AcuerdoDto> GetAcuerdoById(int id)
        {
            var acuerdo = await _acuerdos.GetAcuerdoById(id);
            
            acuerdo.Elaboro = await _usuarios.GetUsuarioByIdAsync(acuerdo.ElaboroId);
            acuerdo.Estatus = await _estatus.GetEstatusById(acuerdo.EstatusId);
            acuerdo.Area = await _areas.GetAreaByIdAsync(acuerdo.AreaId);
            acuerdo.Mes = await _meses.GetMesById(acuerdo.MesId);

            return acuerdo;
        }
    }
}
