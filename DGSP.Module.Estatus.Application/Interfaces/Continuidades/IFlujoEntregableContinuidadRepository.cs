using DGSP.Module.Estatus.Domain.Continuidade;

namespace DGSP.Module.Estatus.Application.Interfaces.Continuidades
{
    public interface IFlujoEntregableContinuidadRepository
    {
        Task<List<FlujoEntregableContinuidad>> GetAllFlujosEntregablesContinuidades();
        Task<List<FlujoEntregableContinuidad>> GetEstatusConsecutivos(int estatus);
    }
}
