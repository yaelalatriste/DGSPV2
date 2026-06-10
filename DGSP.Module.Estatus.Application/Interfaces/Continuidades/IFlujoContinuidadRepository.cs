using DGSP.Module.Estatus.Domain.Continuidade;

namespace DGSP.Module.Estatus.Application.Interfaces.Continuidades
{
    public interface IFlujoContinuidadRepository
    {
        Task<List<FlujoContinuidad>> GetAllFlujosContinuidades();
        Task<List<FlujoContinuidad>> GetEstatusConsecutivos(int estatus);
    }
}
