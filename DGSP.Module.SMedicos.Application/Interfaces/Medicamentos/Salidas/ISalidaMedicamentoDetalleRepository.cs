using DGSP.Module.SMedicos.Domain.Inventarios;

namespace DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Salidas
{
    public interface ISalidaMedicamentoDetalleRepository
    {
        Task<List<SalidaMedicamentoDetalle>> GetDetallesBySalidaIdAsync(int salidaId);
        Task<SalidaMedicamentoDetalle> GetByIdAsync(int id);
        Task AddAsync(SalidaMedicamentoDetalle entity);
        void Remove(SalidaMedicamentoDetalle entity);
        Task SaveChangesAsync();
    }
}
