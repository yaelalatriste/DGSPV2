using DGSP.Module.Catalogos.Application.Services.Generales;
using DGSP.Shared.Contracts.DTOs.Catalogos.Generales;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Catalogos.Queries.Generales
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/catalogos/areas")]
    public class CTAreaController : ControllerBase
    {
        private readonly ICTAreaService _areas;

        public CTAreaController(ICTAreaService areas)
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
