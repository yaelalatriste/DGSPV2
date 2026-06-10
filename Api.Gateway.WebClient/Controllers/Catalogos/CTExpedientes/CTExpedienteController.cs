using Api.Gateway.Models.Catalogos.DTOs.CTExpedientes;
using Api.Gateway.Proxies.Catalogos.CTExpedientes.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Catalogos.CTExpedientes
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("catalogos/ctexpedientes")]
    public class CTExpedienteController : ControllerBase
    {
        private readonly IQCTExpedienteProxy _ctExpedientes;

        public CTExpedienteController(IQCTExpedienteProxy ctExpedientes)
        {
            _ctExpedientes = ctExpedientes;
        }

        [HttpGet]
        [Route("getAllExpedientesAsync")]
        public async Task<List<CTExpedienteDto>> GetCTExpedientesAsync()
        {
            var entregables = await _ctExpedientes.GetAllExpedientesAsync();

            return entregables;
        }

        [HttpGet]
        [Route("getExpedientesByAsunto/{asunto}")]
        public async Task<List<CTExpedienteDto>> GetEntregablesByAsunto(int asunto)
        {
            var entregables = await _ctExpedientes.GetExpedientesByAsunto(asunto);
            
            return entregables;
        }

        [HttpGet]
        [Route("getExpedienteById/{id}")]
        public async Task<CTExpedienteDto> GetEntregableById(int id)
        {
            var entregables = await _ctExpedientes.GetExpedienteById(id);

            return entregables;
        }
    }
}
