using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Salidas;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Salidas;

namespace DGSP.Module.SMedicos.Application.Services.Medicamentos.Salidas
{
    public interface ISalidaMedicamentoService
    {
        Task<List<SalidaMedicamentoDto>> GetAllSalidasAsync();
        Task<SalidaMedicamentoDto> ObtenerPorIdAsync(int id);
        Task<SalidaMedicamentoDto> RegistrarSalidaMedicamentoAsync(RegistrarSalidaMedicamentoCommand command);
    }
}
