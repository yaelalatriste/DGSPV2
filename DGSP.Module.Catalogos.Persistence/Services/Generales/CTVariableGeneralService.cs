using DGSP.Module.Catalogos.Application.Interfaces.Generales;
using DGSP.Module.Catalogos.Application.Services.Generales;
using DGSP.Shared.Contracts.DTOs.Catalogos.Generales;

namespace DGSP.Module.Catalogos.Persistence.Services.Generales
{

    public class CTVariableGeneralService : ICTVariableGeneralService
    {
        private readonly ICTVariableGeneralRepository _cTVariableGeneralRepository;

        public CTVariableGeneralService(ICTVariableGeneralRepository cTVariableGeneralRepository)
        {
            _cTVariableGeneralRepository = cTVariableGeneralRepository;
        }

        public async Task<List<CTVariableGeneralDto>> GetAllVariablesGeneralesAsync()
        {
            var variables = await _cTVariableGeneralRepository.GetAllVariablesGeneralesAsync();

            return variables.Select(v => new CTVariableGeneralDto
            {
                Id = v.Id,
                Anio = v.Anio,
                Abreviacion = v.Abreviacion,
                Nombre = v.Nombre,
                Descripcion = v.Descripcion,
                Valor = v.Valor,
                FechaCreacion = v.FechaCreacion,
            }).ToList();
        }

        public async Task<CTVariableGeneralDto> GetVariableGeneralById(int id)
        {
            var variable = await _cTVariableGeneralRepository.GetVariableGeneralById(id);

            return new CTVariableGeneralDto
            {
                Id = variable.Id,
                Anio = variable.Anio,
                Abreviacion = variable.Abreviacion,
                Nombre = variable.Nombre,
                Descripcion= variable.Descripcion,
                Valor = variable.Valor,
                FechaCreacion= variable.FechaCreacion,
            };
        }

        public async Task<CTVariableGeneralDto> GetVariableGeneralxAnioAbreviacion(int anio, string abreviacion)
        {
            var variable = await _cTVariableGeneralRepository.GetVariableGeneralxAnioAbreviacion(anio, abreviacion);

            return new CTVariableGeneralDto
            {
                Id = variable.Id,
                Anio = variable.Anio,
                Abreviacion = variable.Abreviacion,
                Nombre = variable.Nombre,
                Descripcion = variable.Descripcion,
                Valor = variable.Valor,
                FechaCreacion = variable.FechaCreacion,
            };
        }
    }
}
