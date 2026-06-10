using Api.Gateway.Models.Catalogos.DTOs.CTAreas;
using Api.Gateway.Proxies.Catalogos.CTCendis.Queries;
using DGSP.Module.Catalogos.Application.DTOs.CTCendi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Catalogos.CTAreas
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("catalogos/cendis")]
    public class CTCendisController : ControllerBase
    {
        private readonly IQCTCendisProxy _cendis;

        public CTCendisController(IQCTCendisProxy cendis)
        {
            _cendis = cendis;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getAllCendis")]
        public async Task<List<CTCendiDto>> GetAllCendisAsync()
        {
            var cendis = await _cendis.GetAllCendisAsync();

            return cendis;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getCendisById/{id}")]
        public async Task<CTCendiDto> GetCendisByIdAsync(int id)
        {
            var cendis = await _cendis.GetCendisByIdAsync(id);

            return cendis;
        }
    }
}
