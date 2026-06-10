using Api.Gateway.Models.Catalogos.DTOs.CTAreas;
using Api.Gateway.Models.Catalogos.DTOs.CTIncidencias;
using Api.Gateway.Proxies.Catalogos.CTCendis.Queries;
using Api.Gateway.Proxies.Catalogos.CTIncidencias;
using DGSP.Module.Catalogos.Application.DTOs.CTCendi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Catalogos.CTIncidencias
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("catalogos/incidencias")]
    public class CTIncidenciasController : ControllerBase
    {
        private readonly IQCTIncidenciaProxy _incidencias;

        public CTIncidenciasController(IQCTIncidenciaProxy incidencias)
        {
            _incidencias = incidencias;
        }

        [HttpGet]
        [Route("getAllIncidencias")]
        public async Task<List<CTIncidenciaDto>> GetAllCTIncidenciasAsync()
        {
            var incidencias = await _incidencias.GetAllCTIncidenciasAsync();

            return incidencias;
        }

        [HttpGet]
        [Route("getIncidenciasByTipo/{tipo}")]
        public async Task<List<CTIncidenciaDto>> GetIncidenciasByTipo(string tipo)
        {
            var incidencias = await _incidencias.GetCTIncidenciasByTipo(tipo);

            return incidencias;
        } 
        
        [HttpGet]
        [Route("getIncidenciaById/{id}")]
        public async Task<CTIncidenciaDto> GetCendisByIdAsync(int id)
        {
            var incidencias = await _incidencias.GetCTIncidenciaById(id);

            return incidencias;
        }
    }
}
