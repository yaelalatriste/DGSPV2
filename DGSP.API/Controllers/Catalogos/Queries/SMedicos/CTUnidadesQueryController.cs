using DGSP.Module.Catalogos.Application.Services.SMedicos;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Catalogos.Queries.SMedicos
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/catalogos/[controller]")]
    public class CTUnidadesController
    {
        private readonly ICTUnidadService _unidades;

        public CTUnidadesController(ICTUnidadService unidades)
        {
            _unidades = unidades;
        }

        [HttpGet]
        [Route("getAllUnidades")]
        public async Task<List<CTUnidadDto>> GetAllUnidades()
        {
            var unidades = await _unidades.GetAllUnidadesAsync();

            return unidades;
        }
        
        [HttpGet]
        [Route("GetUnidadById/{id}")]
        public async Task<CTUnidadDto> GetUnidadById(int id)
        {
            var unidad = await _unidades.GetUnidadByIdAsync(id);

            return unidad;
        }
    }
}
