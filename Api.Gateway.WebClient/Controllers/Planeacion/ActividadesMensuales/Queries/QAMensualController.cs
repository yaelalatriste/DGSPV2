using Api.Gateway.Models.Planeacion.Queries.ActividadesMensuales;
using Api.Gateway.Proxies.Catalogos.CTActividades.Queries;
using Api.Gateway.Proxies.Catalogos.CTEntregables.Queries;
using Api.Gateway.Proxies.Catalogos.CTMeses.Queries;
using Api.Gateway.Proxies.Planeacion.Queries.ActividadesMensuales;
using Api.Gateway.Proxies.Planeacion.Queries.Entregables;
using Api.Gateway.Proxies.Usuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Planeacion.ActividadesMensuales.Queries
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("planeacion/actividadesMensuales")]
    public class QAMensualController : ControllerBase
    {
        private readonly IQAMensualProxy _actMensuales;
        private readonly IUsuarioProxy _usuarios;
        private readonly IQCTMesProxy _meses;
        private readonly IQCTEntregableProxy _ctentregables;
        private readonly IQEntregablesAMProxy _entregablesAM;
        private readonly IQCTActividadProxy _actividades;

        public QAMensualController(IQAMensualProxy actMensuales, IQCTMesProxy meses, IQCTEntregableProxy ctentregables, IQEntregablesAMProxy entregablesAM, 
            IQCTActividadProxy actividades, IUsuarioProxy usuarios)
        {
            _actMensuales = actMensuales;
            _meses = meses;
            _ctentregables = ctentregables;
            _entregablesAM = entregablesAM;
            _actividades = actividades;
            _usuarios = usuarios;
        }

        [HttpGet]
        public async Task<List<ActividadMensualDto>> GetAllActividadesAsync()
        {
            var actividades = await _actMensuales.GetAllActividadesAsync();

            return actividades;
        }

        [HttpGet]
        [Route("getActividadesByArea/{area}")]
        public async Task<List<ActividadMensualDto>> GetActividadesByArea(int area)
        {
            var actividades = await _actMensuales.GetActividadesByArea(area);

            foreach (var ac in actividades)
            {
                ac.Mes = await _meses.GetMesById(ac.MesId);
                ac.Actividad = await _actividades.GetActividadById(ac.ActividadId);
                ac.Entregables = await _entregablesAM.GetEntregablesAMByActividad(ac.Id);
                ac.Usuario = await _usuarios.GetUsuarioByIdAsync(ac.UsuarioId);
                foreach (var en in ac.Entregables)
                {
                    en.Entregable = await _ctentregables.GetEntregableById(en.EntregableId);
                }
            }

            return actividades;
        }

        [HttpGet]
        [Route("getActividadesByProceso/{actividad}")]
        public async Task<List<ActividadMensualDto>> GetRegistrosByActividades(int actividad)
        {
            var actividades = await _actMensuales.GetRegistrosByActividades(actividad);

            foreach (var ac in actividades)
            {
                ac.Mes = await _meses.GetMesById(ac.MesId);
                ac.Actividad = await _actividades.GetActividadById(ac.ActividadId);
            }

            return actividades;
        }
    }
}
