using DGSP.Shared.Contracts.DTOs.Estatus.Continuidades;

namespace DGSP.Module.Estatus.Application.Services.NotasTraspaso
{
    public interface IFlujoContinuidadService
    {
        Task<List<FlujoContinuidadDto>> GetAllFlujosContinuidades();
        Task<List<FlujoContinuidadDto>> GetEstatusConsecutivos(int estatus);
    }
}
