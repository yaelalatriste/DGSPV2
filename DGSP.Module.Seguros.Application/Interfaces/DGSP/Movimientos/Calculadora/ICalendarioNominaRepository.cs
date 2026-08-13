using DGSP.Module.Seguros.Domain.DGSP.Movimientos.Calculadora;

namespace DGSP.Module.Seguros.Application.Interfaces.DGSP.Movimientos.Calculadora
{
    public interface ICalendarioNominaRepository
    {
        Task<List<CalendarioNomina>> GetAllCalendarioAsync();
        Task<CalendarioNomina> GetQuincenaById(int id);
        Task<List<CalendarioNomina>> GetQuincenasByPeriodoAsync(string fechaInicial, string fechaFinal);
    }
}
