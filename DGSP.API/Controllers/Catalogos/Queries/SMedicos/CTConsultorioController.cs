using DGSP.Module.Catalogos.Application.Services.SMedicos;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Catalogos.Queries.SMedicos
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/catalogos/consultorios")]
    public class CTConsultorioController : ControllerBase
    {
        private readonly ICTConsultorioService _consultorios;

        public CTConsultorioController(ICTConsultorioService consultorios)
        {
            _consultorios = consultorios;
        }

        [HttpGet]
        [Route("getAllConsultorios")]
        public async Task<List<CTConsultorioDto>> GetAllConsultoriosAsync()
        {
            var areas = await _consultorios.GetAllConsultoriosAsync();

            return areas;
        }

        [HttpGet]
        [Route("getConsultorioById/{id}")]
        public async Task<CTConsultorioDto> GetConsultorioByIdAsync(int id)
        {
            var area = await _consultorios.GetConsulotorioByIdAsync(id);

            return area;
        }
    }
}
