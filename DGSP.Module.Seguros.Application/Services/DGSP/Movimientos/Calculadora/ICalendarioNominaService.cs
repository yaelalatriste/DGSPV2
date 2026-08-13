using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Movimientos.Calculadora;

namespace DGSP.Module.Seguros.Application.Services.DGSP.Movimientos.Calculadora
{
    public interface ICalendarioNominaService
    {
        Task<List<CalendarioNominaDto>> GetAllCalendarioAsync();
        Task<CalendarioNominaDto> GetQuincenaById(int id);
        Task<List<CalendarioNominaDto>> GetQuincenasByPeriodoAsync(string fechaInicial, string fechaFinal);
    }
}
