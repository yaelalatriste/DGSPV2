using DGSP.Module.Catalogos.Domain.SMedicos;

namespace DGSP.Module.Catalogos.Application.Interfaces.SMedicos
{
    public interface ICTMedicamentoRepository
    {
        Task<List<CTMedicamento>> GetAllMedicamentosAsync();
        Task<CTMedicamento> GetMedicamentoByIdAsync(int id);
        Task RegistrarMedicamentoAsync(CTMedicamento medicamento);
        Task ActualizarMedicamentoAsync(CTMedicamento medicamento);
        Task SaveChangesAsync();
    }
}
