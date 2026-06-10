using Api.Gateway.Models.Catalogos.DTOs.CTMeses;
using Api.Gateway.Proxies.Catalogos.CTMeses.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Catalogos.CTMeses
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("catalogos/meses")]
    public class CTMesesController : ControllerBase
    {
        private readonly IQCTMesProxy _meses;

        public CTMesesController(IQCTMesProxy meses)
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
            var meses = await _meses.GetMesById(id);

            return meses;
        }
    }
}
