using DGSP.Module.SMedicos.Domain.Inventarios;

namespace DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Entradas
{
    public interface ILoteMedicamentoRepository
    {
        Task<bool> ExistsAsync(int consultorioId, int medicamentoId, string lote, DateTime fechaCaducidad);
        Task<LoteMedicamento> GetLoteExistAsync(int consultorioId, int medicamentoId, string lote, DateTime fechaCaducidad);
        Task<LoteMedicamento?> GetByIdAsync(int id);
        Task<List<LoteMedicamento>> GetAllAsync();
        Task<LoteMedicamento> GetDatosByLoteAsync(string lote);
        Task<List<LoteMedicamento>> GetMedicamentosByLoteConsultorioAsync(string lote, int consultorio);
        Task<LoteMedicamento> GetDatosByLoteConsultorioAsync(string lote, int consultorio);
        Task<LoteMedicamento> GetDatosByLoteConsultorioMedicamentoAsync(string lote, int consultorio, int medicamento);
        Task AddAsync(LoteMedicamento entity);
        Task SaveChangesAsync();
    }
}
