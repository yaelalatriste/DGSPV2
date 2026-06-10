using DGSP.Shared.Contracts.DTOs.Estatus.Continuidades;

namespace DGSP.Module.Estatus.Application.Services.NotasTraspaso
{
    public interface IFlujoEntregableContinuidadService
    {
        Task<List<FlujoEntregableContinuidadDto>> GetAllFlujosEntregablesContinuidades();
        Task<List<FlujoEntregableContinuidadDto>> GetEstatusConsecutivos(int estatus);
    }
}
