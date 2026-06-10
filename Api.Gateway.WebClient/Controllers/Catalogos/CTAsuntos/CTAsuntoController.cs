using Api.Gateway.Models.Catalogos.DTOs.CTAsuntos;
using Api.Gateway.Proxies.Catalogos.CTAsuntos.Queries;
using Api.Gateway.Proxies.Catalogos.CTExpedientes.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Catalogos.CTAsuntos
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("catalogos/ctasuntos")]
    public class CTAsuntoController : ControllerBase
    {
        private readonly IQCTAsuntoProxy _ctAsuntos;

        public CTAsuntoController(IQCTAsuntoProxy ctAsuntos)
        {
            _ctAsuntos = ctAsuntos;
        }

        [HttpGet]
        [Route("getAllAsuntosAsync")]
        public async Task<List<CTAsuntoDto>> GetAllAsuntosAsync()
        {
            var asuntos = await _ctAsuntos.GetAllAsuntosAsync();

            return asuntos;
        }

        [HttpGet]
        [Route("getAsuntosByArea/{area}")]
        public async Task<List<CTAsuntoDto>> GetAsuntosByArea(int area)
        {
            var asuntos = await _ctAsuntos.GetAsuntosByArea(area);

            return asuntos;
        }

        [HttpGet]
        [Route("getAsuntoById/{id}")]
        public async Task<CTAsuntoDto> GetAsuntoById(int id)
        {
            var entregables = await _ctAsuntos.GetAsuntoById(id);

            return entregables;
        }
    }
}
