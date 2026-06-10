using DGSP.Module.Catalogos.Application.Services.Generales;
using DGSP.Shared.Contracts.DTOs.Catalogos.Generales;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Catalogos.Queries.Generales
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/catalogos/meses")]
    public class CTMesController : ControllerBase
    {
        private readonly ICTMesService _meses;

        public CTMesController(ICTMesService meses)
        {
            _meses = meses;
        }

        [HttpGet]
        public async Task<List<CTMesDto>> GetAllMeses()
        {
            var meses = await _meses.GetAllMesesAsync();

            return meses;
        }

        [HttpGet]
        [Route("getMesById/{id}")]
        public async Task<CTMesDto> GetMesByid(int id)
        {
            var meses = await _meses.GetMesByIdAsync(id);
            return meses;
        }
    }
}
