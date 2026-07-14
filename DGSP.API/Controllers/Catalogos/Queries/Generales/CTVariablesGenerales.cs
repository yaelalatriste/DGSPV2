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
    public class CTVariablesGeneralesController : ControllerBase
    {
        private readonly ICTVariableGeneralService _variablesGenerales;

        public CTVariablesGeneralesController(ICTVariableGeneralService variablesGenerales)
        {
            _variablesGenerales = variablesGenerales;
        }

        [HttpGet]
        [Route("getAllVariablesGenerales")]
        public async Task<List<CTVariableGeneralDto>> GetAllVariablesGeneralesAsync()
        {
            var variable = await _variablesGenerales.GetAllVariablesGeneralesAsync();

            return variable;
        }

        [HttpGet]
        [Route("getVariableGeneralById/{id}")]
        public async Task<CTVariableGeneralDto> GetVariableGeneralById(int id)
        {
            var variable = await _variablesGenerales.GetVariableGeneralById(id);
            return variable;
        }
        
        [HttpGet]
        [Route("getVariableByAnioxAbreviacion/{anio}/{abreviacion}")]
        public async Task<CTVariableGeneralDto> GetMesByid(int anio, string abreviacion)
        {
            var variable = await _variablesGenerales.GetVariableGeneralxAnioAbreviacion(anio, abreviacion);

            return variable;
        }
    }
}
