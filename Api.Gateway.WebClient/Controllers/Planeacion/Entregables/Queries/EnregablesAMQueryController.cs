using Api.Gateway.Models.Planeacion.Queries.Entregables;
using Api.Gateway.Proxies.Catalogos.CTEntregables.Queries;
using Api.Gateway.Proxies.Planeacion.Queries.Entregables;
using Api.Gateway.Proxies.Usuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Planeacion.Entregables.Queries
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("planeacion/entregablesAM")]
    public class EnregablesAMQueryController : ControllerBase
    {
        private readonly IQEntregablesAMProxy _entregables;
        private readonly IQCTEntregableProxy _ctentregables;
        private readonly IUsuarioProxy _usuarios;
        private readonly IHostingEnvironment _environment;

        public EnregablesAMQueryController(IQEntregablesAMProxy entregables, IHostingEnvironment environment, IUsuarioProxy usuarios, IQCTEntregableProxy ctentregables)
        {
            _entregables = entregables;
            _ctentregables = ctentregables;
            _usuarios = usuarios;
            _environment = environment;
        }

        [HttpGet]
        [Route("getAllEntregablesAM")]
        public async Task<List<EntregableAMDto>> GetAllEntregables()
        {
            var entregables = await _entregables.GetAllEntregables();
            return entregables;
        }

        [HttpGet]
        [Route("getEntregablesAMByActividad/{actividad}")]
        public async Task<List<EntregableAMDto>> GetEntregablesAMByActividad(int actividad)
        {
            var entregables = await _entregables.GetEntregablesAMByActividad(actividad);
            foreach (var en in entregables)
            {
                en.Entregable = await _ctentregables.GetEntregableById(en.EntregableId);
                en.Usuario = await _usuarios.GetUsuarioByIdAsync(en.UsuarioId);
            }
            return entregables;
        }

        [Route("visualizarEntregable/{anio}/{proceso}/{actividad}/{mes}/{entregable}/{archivo}")]
        [HttpGet]
        public async Task<string> VisualizarEntregable(int anio, string proceso, string actividad, string mes, string entregable, string archivo)
        {
            var ruta = await _entregables.VisualizarEntregable(anio, proceso, actividad, mes, entregable, archivo);

            if (System.IO.File.Exists(ruta))
            {
                return ruta;
            }

            return "";
        }
    }
}
