using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;

namespace DGSP.Module.Catalogos.Application.Services.SMedicos
{
    public interface ICTVariableMedicaService
    {
        Task<List<CTVariableMedicaDto>> GetAllVariablesAsync();
        Task<CTVariableMedicaDto> GetVariableById(int id);
        Task<List<CTVariableMedicaDto>> GetVariablesByCategoria(string categoria);
    }
}
