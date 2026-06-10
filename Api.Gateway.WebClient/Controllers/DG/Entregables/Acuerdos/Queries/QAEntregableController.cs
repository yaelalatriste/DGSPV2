using Api.Gateway.Models.DG.Queries.AEntregable;
using Api.Gateway.Proxies.Catalogos.CTArchivos;
using Api.Gateway.Proxies.DG.Queries.AEntregables;
using Api.Gateway.Proxies.Usuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


namespace Api.Gateway.WebClient.Controllers.DG.Entregables.Acuerdos.Queries
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("dg/entregablesAcuerdos")]
    public class QAEntregableController : ControllerBase
    {
        private readonly IQAEntregableProxy _entregables;
        private readonly IUsuarioProxy _usuarios;
        private readonly IQCTArchivoProxy _ctarchivos;
        private readonly IHostingEnvironment _environment;

        public QAEntregableController(IQAEntregableProxy entregables, IUsuarioProxy usuarios, IQCTArchivoProxy ctarchivos, IHostingEnvironment environment)
        {
            _entregables = entregables;
            _usuarios = usuarios;
            _ctarchivos = ctarchivos;
            _environment = environment;
        }

        [HttpGet]
        public async Task<List<AEntregableDto>> GetAllAcuerdosAsync()
        {
            var acuerdos = await _entregables.GetAllEntregablesAsync();

            return acuerdos;
        }

        [HttpGet]
        [Route("getAEntregableByAcuerdo/{acuerdo}")]
        public async Task<List<AEntregableDto>> GetAEntregableByAcuerdo(int acuerdo)
        {
            var entregables = await _entregables.GetEntregablesByAcuerdo(acuerdo);
            foreach (var en in entregables)
            {
                en.Usuario = await _usuarios.GetUsuarioByIdAsync(en.UsuarioId);
                en.Entregable = await _ctarchivos.GetArchivoById(en.EntregableId);
            }
            return entregables;
        }
        
        [HttpGet]
        [Route("getAcuerdoById/{id}")]
        public async Task<AEntregableDto> GetAcuerdoById(int id)
        {
            var entregable = await _entregables.GetEntregableById(id);

            entregable.Usuario = await _usuarios.GetUsuarioByIdAsync(entregable.UsuarioId);
            entregable.Entregable = await _ctarchivos.GetArchivoById(entregable.EntregableId);

            return entregable;
        }

        [Route("visualizarEntregable/{anio}/{mes}/{area}/{folio}/{entregable}/{archivo}")]
        [HttpGet]
        public string VisualizarEntregable(int anio, string mes, string area, string folio, string entregable, string archivo)
        {
            string folderName = "\\\\cjfzrep_fs\\dgsp\\DG\\Sistemas\\Acuerdos\\" + area + "\\" + anio + "\\" + mes+"\\"+ folio+ "\\" + entregable;
            string webRootPath = _environment.ContentRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            string pathArchivo = Path.Combine(newPath, archivo);

            if (System.IO.File.Exists(pathArchivo))
            {
                return pathArchivo;
            }
            return "";
        }
    }
}
