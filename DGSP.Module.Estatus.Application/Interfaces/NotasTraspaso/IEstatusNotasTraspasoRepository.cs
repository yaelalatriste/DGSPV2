using DGSP.Module.Estatus.Domain.NotasTraspaso;

namespace DGSP.Module.Estatus.Application.Interfaces.NotasTraspaso
{
    public interface IEstatusNotasTraspasoRepository
    {
        Task<List<ENotaTraspaso>> GetAllEstatus();
        Task<ENotaTraspaso> GetEstatusById(int id);
    }
}
