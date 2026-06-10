using Api.Gateway.Models.Planeacion.Queries.Remisiones;
using Api.Gateway.Proxies.Planeacion.Queries.Remisiones;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Planeacion.Remisiones.Queries
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("planeacion/remisiones")]
    public class RemisionQueryController : ControllerBase
    {
        private readonly IQRemisionProxy _remisiones;

        public RemisionQueryController(IQRemisionProxy remisiones)
        {
            _remisiones = remisiones;
        }

        [HttpGet]
        [Route("getAllRemisiones")]
        public async Task<List<RemisionDto>> GetAllRemisiones()
        {
            var remisiones = await _remisiones.GetAllRemisiones();

            return remisiones;
        }

        [HttpGet]
        [Route("getRemisionById/{id}")]
        public async Task<RemisionDto> GetRemisionById(int id)
        {
            var remisiones = await _remisiones.GetRemisionById(id);

            return remisiones;
        }
    }
}
