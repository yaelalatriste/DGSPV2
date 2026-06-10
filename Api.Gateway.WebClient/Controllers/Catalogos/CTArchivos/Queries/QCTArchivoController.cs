using Api.Gateway.Models.Catalogos.DTOs.CTArchivos;
using Api.Gateway.Proxies.Catalogos.CTArchivos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Catalogos.CTArchivos.Queries
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("catalogos/ctarchivos")]
    public class QCTArchivoController : ControllerBase
    {
        private readonly IQCTArchivoProxy _ctarchivos;

        public QCTArchivoController(IQCTArchivoProxy ctarchivos)
        {
            _ctarchivos = ctarchivos;
        }

        [HttpGet]
        [Route("getAllArchivos")]
        public async Task<List<CTArchivoDto>> GetAllArchivosAsync()
        {
            var archivos = await _ctarchivos.GetAllArchivosAsync();

            return archivos;
        }

        [HttpGet]
        [Route("getArchivosByModulo/{modulo}")]
        public async Task<List<CTArchivoDto>> GetArchivosByModuloAsync(int modulo)
        {
            var archivos = await _ctarchivos.GetArchivosByModuloAsync(modulo);

            return archivos;
        }

        [HttpGet]
        [Route("getArchivosBySubmodulo/{submodulo}")]
        public async Task<List<CTArchivoDto>> GetArchivosBySubmoduloAsync(int submodulo)
        {
            var archivos = await _ctarchivos.GetArchivosBySubmoduloAsync(submodulo);

            return archivos;
        }

        [HttpGet]
        [Route("getArchivosByModSub/{modulo}/{submodulo}")]
        public async Task<List<CTArchivoDto>> GetArchivosByModSubAsync(int modulo, int submodulo)
        {
            var archivos = await _ctarchivos.GetArchivosByModSubAsync(modulo, submodulo);

            return archivos;
        }

        [HttpGet]
        [Route("getArchivoById/{id}")]
        public async Task<CTArchivoDto> GetArchivoById(int id)
        {
            var archivo = await _ctarchivos.GetArchivoById(id);

            return archivo;
        }
    }
}
