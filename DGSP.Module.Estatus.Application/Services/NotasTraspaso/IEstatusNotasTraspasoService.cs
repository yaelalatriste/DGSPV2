using DGSP.Shared.Contracts.DTOs.Estatus.NotasTraspaso;

namespace DGSP.Module.Estatus.Application.Services.NotasTraspaso
{
    public interface IEstatusNotasTraspasoService
    {
        Task<List<ENotaTraspasoDto>> GetAllEstatus();
        Task<ENotaTraspasoDto> GetEstatusById(int id);
    }
}
