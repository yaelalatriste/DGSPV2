using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Salidas;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Salidas;

namespace DGSP.Module.SMedicos.Application.Services.Medicamentos.Salidas
{
    public interface ISalidaMedicamentoDetalleService
    {
        Task<List<SalidaMedicamentoDetalleDto>> GetDetallesBySalidaAsync(int salida);
        Task<SalidaMedicamentoDetalleDto> GetDetalleByIdAsync(int id);
        Task<SalidaMedicamentoDetalleDto> RegistrarDetalleSalidaMedicamentoAsync(RegistrarSalidaMedicamentoDetalleCommand command);
    }
}
