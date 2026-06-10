using DGSP.Module.Catalogos.Application.Services.Generales;
using DGSP.Shared.Contracts.DTOs.Catalogos.Generales;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Catalogos.Queries.Generales
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/catalogos/[controller]")]
    public class CTEntregablesController : ControllerBase
    {
        private readonly ICTEntregableService _Entregables;

        public CTEntregablesController(ICTEntregableService Entregables)
        {
            _Entregables = Entregables;
        }

        [HttpGet]
        public async Task<List<CTEntregableDto>> GetAllEntregablesAsync()
        {
            var Entregables = await _Entregables.GetAllEntregablesAsync();

            return Entregables;
        }

        [HttpGet]
        [Route("getEntregableById/{id}")]
        public async Task<CTEntregableDto> GetAllEntregablesAsync(int id)
        {
            var Entregable = await _Entregables.GetEntregableByIdAsync(id);

            return Entregable;
        }
    }
}
