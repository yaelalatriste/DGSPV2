using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;

namespace DGSP.Module.SMedicos.Application.Services.Medicamentos.NotasTraspaso
{
    public interface IDetalleNotaTraspasoQueryService
    {
        Task<List<DetalleNotaTraspasoDto>> GetDetallesNotaTraspasoByNotaAsync(int nota);
        Task<DetalleNotaTraspasoDto> GetDetalleNotaTraspasoByIdAsync(int id);
        Task<DetalleNotaTraspasoDto> AddDetalleNotaTraspaso(RegistrarDetalleNotaTraspasoCommand command);
        Task<DetalleNotaTraspasoDto> UpdateDetalleNotaTraspaso(ActualizarDetalleNotaTraspasoCommand command);
        Task<DetalleNotaTraspasoDto> DeleteDetalleNotaTraspaso(EliminarDetalleNotaTraspasoCommand command);
        Task EliminarDetalleNotaTraspaso(int id);
    }
}
