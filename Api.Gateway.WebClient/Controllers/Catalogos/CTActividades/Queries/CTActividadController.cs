using Api.Gateway.Models.Catalogos.DTOs.CTActividades;
using Api.Gateway.Proxies.Catalogos.CTActividades.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Catalogos.CTActividades.Queries
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("catalogos/actividades")]
    public class CTActividadController : ControllerBase
    {
        private readonly IQCTActividadProxy _actividades;

        public CTActividadController(IQCTActividadProxy actividades)
        {
            _actividades = actividades;
        }

        [HttpGet]
        public async Task<List<CTActividadDto>> GetAllActividadesAsync()
        {
            var actividades = await _actividades.GetAllActividadesAsync();

            return actividades;
        }



        [HttpGet]
        [Route("getActividadesByProceso/{proceso}")]
        public async Task<List<CTActividadDto>> GetActividadesByProceso(int proceso)
        {
            var actividades = await _actividades.GetActividadesByProceso(proceso);

            return actividades;
        }

        [HttpGet]
        [Route("getActividadById/{id}")]

        public async Task<CTActividadDto> GetActividadById(int id)
        {
            var actividades = await _actividades.GetActividadById(id);

            return actividades;
        }
    }
}
