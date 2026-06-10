using DGSP.Module.Estatus.Domain.NotasTraspaso;

namespace DGSP.Module.Estatus.Application.Interfaces.NotasTraspaso
{
    public interface IFlujoNotaTraspasoRepository
    {
        Task<List<FlujoNotaTraspaso>> GetAllFlujosNotasTraspaso();
        Task<List<FlujoNotaTraspaso>> GetEstatusConsecutivos(int estatus);
    }
}
