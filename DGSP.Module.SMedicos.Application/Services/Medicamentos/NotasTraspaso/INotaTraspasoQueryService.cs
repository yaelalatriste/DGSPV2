using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;

namespace DGSP.Module.SMedicos.Application.Services.Medicamentos.NotasTraspaso
{
    public interface INotaTraspasoQueryService
    {
        Task<List<NotaTraspasoDto>> GetAllNotasTraspasoAsync();
        Task<NotaTraspasoDto> GetNotaTraspasoByIdAsync(int id);
        Task<NotaTraspasoDto> AddNotaTraspaso(RegistrarNotaTraspasoCommand command);
        Task<NotaTraspasoDto> ActualizarNotaTraspaso(ActualizarNotaTraspasoCommand command);
        Task<NotaTraspasoDto> ConcluirNotaTraspaso(ConcluirNotaTraspasoCommand command);
    }
}
