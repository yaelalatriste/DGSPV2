using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Entradas;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Entradas;

namespace DGSP.Module.SMedicos.Application.Services.Medicamentos.Entradas
{
    public interface IInventarioAppService
    {
        Task<LoteDto> RegistrarLoteAsync(RegistrarLoteMedicamentoCommand command);
        Task<List<LoteDto>> ConsultarLotesAsync();
        Task<LoteDto> GetLoteByIdAsync(int id);
        Task<LoteDto> GetDatosByLoteAsync(string lote);
        Task<LoteDto> GetDatosByLoteConsultorioAsync(string lote, int consultorio);
        Task<LoteDto> GetDatosByLoteConsultorioMedicamentoAsync(string lote, int consultorio, int medicamento);
        Task<List<LoteDto>> GetMedicamentosByLoteConsultorioAsync(string lote, int consultorio);
    }
}
