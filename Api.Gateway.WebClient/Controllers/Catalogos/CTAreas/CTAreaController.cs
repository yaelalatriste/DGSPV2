using Api.Gateway.Models.Catalogos.DTOs.CTAreas;
using Api.Gateway.Proxies.Catalogos.CTAreas.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Catalogos.CTAreas
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("catalogos/areas")]
    public class CTAreaController : ControllerBase
    {
        private readonly IQCTAreaProxy _areas;

        public CTAreaController(IQCTAreaProxy areas)
        {
            _areas = areas;
        }

        [HttpGet]
        public async Task<List<CTAreaDto>> GetAllAreasAsync()
        {
            var areas = await _areas.GetAllAreasAsync();

            return areas;
        }

        [HttpGet]
        [Route("getAreaById/{id}")]
        public async Task<CTAreaDto> GetAllAreasAsync(int id)
        {
            var area = await _areas.GetAreaByIdAsync(id);

            return area;
        }
    }
}
