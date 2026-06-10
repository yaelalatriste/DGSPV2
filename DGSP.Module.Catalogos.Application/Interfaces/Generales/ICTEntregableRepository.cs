using DGSP.Module.Catalogos.Domain.Generales;

namespace DGSP.Module.Catalogos.Application.Interfaces.Generales
{
    public interface ICTEntregableRepository
    {
        Task<List<CTEntregable>> GetAllEntregablesAsync();
        Task<CTEntregable> GetEntregableByIdAsync(int id);
    }
}
