using System.IO;
using System.Threading.Tasks;
using Api.Gateway.Proxies.Catalogos.CTAsuntos.Queries;
using Api.Gateway.Proxies.Catalogos.CTCategorias.Queries;
using Api.Gateway.Proxies.Catalogos.CTExpedientes.Queries;
using Api.Gateway.Proxies.Planeacion.Queries.Expedientes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.WebClient.Controllers.Planeacion.Expedientes.Queries
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("planeacion/expedientes")]
    public class QExpedienteController : ControllerBase
    {
        private readonly IQExpedientePlaneacionProxy _expediente;
        private readonly IQCTCategoriaProxy _categorias;
        private readonly IQCTAsuntoProxy _asuntos;
        private readonly IQCTExpedienteProxy _expedientes;
        private readonly IHostingEnvironment _environment;

        public QExpedienteController(IQExpedientePlaneacionProxy expediente, IQCTCategoriaProxy categorias, IQCTAsuntoProxy asuntos, IQCTExpedienteProxy expedientes, 
            IHostingEnvironment environment)
        {
            _expediente = expediente;
            _categorias = categorias;
            _asuntos = asuntos;
            _expedientes = expedientes;
            _environment = environment;
        }

        [HttpGet]
        [Route("getAllExpedientes")]
        public async Task<IActionResult> GetAllExpedientes()
        {
            var expedientes = await _expediente.GetAllExpedientes();
            foreach (var ex in expedientes)
            {
                ex.Categoria = await _categorias.GetCategoriaByIdAsync(ex.CategoriaId);
                ex.Expediente = await _expedientes.GetExpedienteById(ex.EntregableId);
                ex.Expediente.Asunto = await _asuntos.GetAsuntoById(ex.Expediente.AsuntoId);
            }
            return Ok(expedientes);
        }

        [HttpGet]
        [Route("getExpedientesByAnio/{anio}")]
        public async Task<IActionResult> GetExpedientesByAnio(int anio)
        {
            var expedientes = await _expediente.GetExpedientesByAnio(anio);
            foreach (var ex in expedientes)
            {
                ex.Categoria = await _categorias.GetCategoriaByIdAsync(ex.CategoriaId);
                ex.Expediente = await _expedientes.GetExpedienteById(ex.EntregableId);
                ex.Expediente.Asunto = await _asuntos.GetAsuntoById(ex.Expediente.AsuntoId);
            }
            return Ok(expedientes);
        }
        
        [HttpGet]
        [Route("getExpedientesByCategoria/{categoria}")]
        public async Task<IActionResult> GetExpedientesByAnioCategoria(int categoria)
        {
            var expedientes = await _expediente.GetExpedientesByCategoria(categoria);
            foreach (var ex in expedientes)
            {
                ex.Categoria = await _categorias.GetCategoriaByIdAsync(ex.CategoriaId);
                ex.Expediente = await _expedientes.GetExpedienteById(ex.EntregableId);
                ex.Expediente.Asunto = await _asuntos.GetAsuntoById(ex.Expediente.AsuntoId);
            }
            return Ok(expedientes);
        }
        
        [HttpGet]
        [Route("getExpedientesByAnioCategoria/{anio}/{categoria}")]
        public async Task<IActionResult> GetExpedientesByAnioCategoria(int anio, int categoria)
        {
            var expedientes = await _expediente.GetExpedientesByAnioCategoria(anio, categoria);
            foreach (var ex in expedientes)
            {
                ex.Categoria = await _categorias.GetCategoriaByIdAsync(ex.CategoriaId);
                ex.Expediente = await _expedientes.GetExpedienteById(ex.EntregableId);
                ex.Expediente.Asunto = await _asuntos.GetAsuntoById(ex.Expediente.AsuntoId);
            }
            return Ok(expedientes);
        }
        
        [HttpGet]
        [Route("getExpedienteById/{id}")]
        public async Task<IActionResult> GetExpedienteById(int id)
        {
            var expedientes = await _expediente.GetExpedienteById(id);

            return Ok(expedientes);
        }

        [Route("visualizarEntregable/{area}/{anio}/{categoria}/{entregable}/{archivo}")]
        [HttpGet]
        public string VisualizarEntregable(string area, int anio, string categoria, string entregable, string archivo)
        {
            string folderName = "\\\\cjfzrep_fs\\dgsp\\DG\\EXPEDIENTES ELECTRÓNICOS\\" + area + "\\" + anio + "\\" + categoria + "\\" + entregable;
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
