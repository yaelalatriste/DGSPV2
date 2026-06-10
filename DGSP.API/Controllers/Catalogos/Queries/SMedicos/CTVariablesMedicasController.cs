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
    public class CTVariablesMedicasController : ControllerBase
    {
        private readonly ICTVariableMedicaService _variables;

        public CTVariablesMedicasController(ICTVariableMedicaService variables)
        {
            _variables = variables;
        }

        [HttpGet]
        [Route("getAllVariablesMedicas")]
        public async Task<List<CTVariableMedicaDto>> GetAllVariablesMedicasAsync()
        {
            var variables = await _variables.GetAllVariablesAsync();

            return variables;
        }

        [HttpGet]
        [Route("getVariableById/{id}")]
        public async Task<CTVariableMedicaDto> GetVariableById(int id)
        {
            var variables = await _variables.GetVariableById(id);

            return variables;
        }
        
        [HttpGet]
        [Route("getVariablesByCategoria/{categoria}")]
        public async Task<List<CTVariableMedicaDto>> GetVariablesByCategoria(string categoria)
        {
            var variables = await _variables.GetVariablesByCategoria(categoria);

            return variables;
        }
    }
}
