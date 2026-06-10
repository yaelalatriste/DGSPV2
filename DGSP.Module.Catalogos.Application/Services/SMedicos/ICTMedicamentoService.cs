using DGSP.Shared.Contracts.Commands.Catalogos.SMedicos.Medicamentos;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;

namespace DGSP.Module.Catalogos.Application.Services.SMedicos
{
    public interface ICTMedicamentoService
    {
        Task<List<CTMedicamentoDto>> GetAllMedicamentosAsync();
        Task<CTMedicamentoDto> GetMedicamentoByIdAsync(int id);
        Task<List<CTMedicamentoDto>> GetMedicamentosByAnioAsync(int anio);
        Task<CTMedicamentoDto> RegistrarMedicamentoAsync(RegistrarCTMedicamentoCommand command);
        Task<CTMedicamentoDto> ActualizarMedicamentoAsync(ActualizarCTMedicamentoCommand command);
    }
}
