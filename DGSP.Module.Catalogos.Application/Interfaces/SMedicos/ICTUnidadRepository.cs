using DGSP.Module.Catalogos.Domain.SMedicos;

namespace DGSP.Module.Catalogos.Application.Interfaces.SMedicos
{
    public interface ICTUnidadRepository
    {
        Task<List<CTUnidad>> GetAllUnidadesAsync();
        Task<CTUnidad> GetUnidadByIdAsync(int id);
    }
}
