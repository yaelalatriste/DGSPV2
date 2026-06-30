using DGSP.Module.Catalogos.Domain.Generales;

namespace DGSP.Module.Catalogos.Application.Interfaces.Generales
{
    public interface ICTVariableGeneralRepository
    {
        Task<List<CTVariableGeneral>> GetAllVariablesGeneralesAsync();
        Task<CTVariableGeneral> GetVariableGeneralById(int id);
        Task<CTVariableGeneral> GetVariableGeneralxAnioAbreviacion(int anio, string abreviacion);
    }
}
