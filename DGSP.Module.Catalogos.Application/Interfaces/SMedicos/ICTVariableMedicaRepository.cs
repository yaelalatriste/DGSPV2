using DGSP.Module.Catalogos.Domain.SMedicos;

namespace DGSP.Module.Catalogos.Application.Interfaces.SMedicos
{
    public interface ICTVariableMedicaRepository
    {
        Task<List<CTVariableMedica>> GetAllVariablesAsync();
        Task<CTVariableMedica> GetVariableById(int id);
        Task<List<CTVariableMedica>> GetVariablesByCategoria(string categoria);
    }
}
