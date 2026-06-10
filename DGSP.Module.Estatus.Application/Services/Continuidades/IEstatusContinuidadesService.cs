using DGSP.Shared.Contracts.DTOs.Estatus.Continuidades;

namespace DGSP.Module.Estatus.Application.Services.Continuidades
{
    public interface IEstatusContinuidadesService
    {
        Task<List<EstatusContinuidadDto>> GetAllEstatus();
        Task<EstatusContinuidadDto> GetEstatusById(int id);
    }
}
