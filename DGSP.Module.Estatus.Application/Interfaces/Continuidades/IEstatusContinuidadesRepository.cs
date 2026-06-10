using DGSP.Module.Estatus.Domain;

namespace DGSP.Module.Estatus.Application.Interfaces.Continuidades
{
    public interface IEstatusContinuidadesRepository
    {
        Task<List<EstatusContinuidad>> GetAllEstatus();
        Task<EstatusContinuidad> GetEstatusById(int id);
    }
}
