using Api.Gateway.Models.Catalogos.DTOs.CTEntregables;
using Api.Gateway.Proxies.Catalogos.CTEntregables.Queries;
using Api.Gateway.Proxies.Catalogos.CTExpedientes.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Catalogos.CTEntregables
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("catalogos/ctentregables")]
    public class CTEntregableController : ControllerBase
    {
        private readonly IQCTEntregableProxy _ctentregables;

        public CTEntregableController(IQCTEntregableProxy ctentregables)
        {
            _ctentregables = ctentregables;
        }

        [HttpGet]
        public async Task<List<CTEntregableDto>> GetCTEntregablesAsync()
        {
            var entregables = await _ctentregables.GetAllEntregablesAsync();

            return entregables;
        }

        [HttpGet]
        [Route("getEntregableById/{id}")]
        public async Task<CTEntregableDto> GetEntregableById(int id)
        {
            var entregables = await _ctentregables.GetEntregableById(id);

            return entregables;
        }
    }
}
