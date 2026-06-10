using DGSP.Shared.Contracts.DTOs.Estatus.NotasTraspaso;

namespace DGSP.Module.Estatus.Application.Services.NotasTraspaso
{
    public interface IFlujoNotaTraspasoService
    {
        Task<List<FlujoNotaTraspasoDto>> GetAllFlujosNotasTraspaso();
        Task<List<FlujoNotaTraspasoDto>> GetEstatusConsecutivos(int estatus);
    }
}
