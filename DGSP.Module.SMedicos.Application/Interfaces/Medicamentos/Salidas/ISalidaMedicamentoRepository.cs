using DGSP.Module.SMedicos.Domain.Inventarios;

namespace DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Salidas
{
    public interface ISalidaMedicamentoRepository
    {
        Task<List<SalidaMedicamento>> GetAllSalidasMedicamentosAsync();
        Task<SalidaMedicamento> GetByIdAsync(int id);
        Task AddAsync(SalidaMedicamento entity);
        Task SaveChangesAsync();
    }
}
