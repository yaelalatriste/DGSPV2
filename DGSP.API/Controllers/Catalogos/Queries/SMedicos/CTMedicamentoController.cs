using DGSP.Module.Catalogos.Application.Services.SMedicos;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Catalogos.Queries.SMedicos
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/catalogos/medicamentos")]
    public class CTMedicamentoController : ControllerBase
    {
        private readonly ICTMedicamentoService _medicamentos;

        public CTMedicamentoController(ICTMedicamentoService medicamentos)
        {
            _medicamentos = medicamentos;
        }

        [HttpGet]
        [Route("getAllMedicamentos")]
        public async Task<List<CTMedicamentoDto>> GetAllMedicamentosAsync()
        {
            var medicamentos = await _medicamentos.GetAllMedicamentosAsync();

            return medicamentos;
        }

        [HttpGet]
        [Route("getMedicamentoById/{id}")]
        public async Task<CTMedicamentoDto> GetAllmedicamentosAsync(int id)
        {
            var medicamento = await _medicamentos.GetMedicamentoByIdAsync(id);

            return medicamento;
        }
        
        [HttpGet]
        [Route("getMedicamentosByAnio/{anio}")]
        public async Task<List<CTMedicamentoDto>> GetMedicamentosByAnio(int anio)
        {
            var medicamentos = await _medicamentos.GetMedicamentosByAnioAsync(anio);

            return medicamentos;
        }
    }
}
