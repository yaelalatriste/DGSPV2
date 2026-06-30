using DGSP.Shared.Contracts.DTOs.Catalogos.Generales;

namespace DGSP.Module.Catalogos.Application.Services.Generales
{
    public interface ICTVariableGeneralService
    {
        Task<List<CTVariableGeneralDto>> GetAllVariablesGeneralesAsync();
        Task<CTVariableGeneralDto> GetVariableGeneralById(int id);
        Task<CTVariableGeneralDto> GetVariableGeneralxAnioAbreviacion(int anio, string abreviacion);
    }
}
