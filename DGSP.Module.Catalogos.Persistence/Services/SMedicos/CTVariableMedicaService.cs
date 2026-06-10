using DGSP.Module.Catalogos.Application.Interfaces.SMedicos;
using DGSP.Module.Catalogos.Application.Services.SMedicos;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;

namespace DGSP.Module.Catalogos.Persistence.Services.SMedicos
{
    public class CTVariableMedicaService : ICTVariableMedicaService
    {
        private readonly ICTVariableMedicaRepository _cTVariableMedicaRepository;

        public CTVariableMedicaService(ICTVariableMedicaRepository cTVariableMedicaRepository)
        {
            _cTVariableMedicaRepository = cTVariableMedicaRepository;   
        }

        public async Task<List<CTVariableMedicaDto>> GetAllVariablesAsync()
        {
            var variables = await _cTVariableMedicaRepository.GetAllVariablesAsync();   

            return variables.Select(v => new CTVariableMedicaDto {
                Id = v.Id,
                Categoria = v.Categoria,
                Abreviacion = v.Abreviacion,
                Nombre = v.Nombre,
                Singular = v.Singular,
                Plural = v.Plural,
            }).ToList();
        }
        
        public async Task<List<CTVariableMedicaDto>> GetVariablesByCategoria(string categoria)
        {
            var variables = await _cTVariableMedicaRepository.GetVariablesByCategoria(categoria);

            return variables.Select(v => new CTVariableMedicaDto
            {
                Id = v.Id,
                Categoria = v.Categoria,
                Abreviacion = v.Abreviacion,
                Nombre = v.Nombre,
                Singular = v.Singular,
                Plural = v.Plural,
            }).ToList();
        }

        public async Task<CTVariableMedicaDto> GetVariableById(int id)
        {
            var v = await _cTVariableMedicaRepository.GetVariableById(id);

            return new CTVariableMedicaDto
            {
                Id = v.Id,
                Categoria = v.Categoria,
                Abreviacion = v.Abreviacion,
                Nombre = v.Nombre,
                Singular = v.Singular,
                Plural = v.Plural,
            };
        }
    }
}
