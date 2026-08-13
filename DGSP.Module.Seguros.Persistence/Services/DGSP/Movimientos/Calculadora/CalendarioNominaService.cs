using DGSP.Module.Seguros.Application.Interfaces.DGSP.Movimientos.Calculadora;
using DGSP.Module.Seguros.Application.Services.DGSP.Movimientos.Calculadora;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Movimientos.Calculadora;

namespace DGSP.Module.Seguros.Persistence.Services.DGSP.Movimientos.Calculadora
{
    public class CalendarioNominaService : ICalendarioNominaService
    {
        private readonly ICalendarioNominaRepository _calendarioNominaRepository;
        public CalendarioNominaService(ICalendarioNominaRepository calendarioNominaRepository)
        {
            _calendarioNominaRepository = calendarioNominaRepository;
        }
        public async Task<List<CalendarioNominaDto>> GetAllCalendarioAsync()
        {
            var result = await _calendarioNominaRepository.GetAllCalendarioAsync();
            return result.Select(x => new CalendarioNominaDto
            {
                Id = x.Id,
                QuincenaRestante = x.QuincenaRestante,
                NumeroQuincena = x.NumeroQuincena,
                FechaInicio = x.FechaInicio,
                FechaFinal = x.FechaFinal,
                Quincena = x.Quincena
            }).ToList();
        }
        public async Task<CalendarioNominaDto> GetQuincenaById(int id)
        {
            var result = await _calendarioNominaRepository.GetQuincenaById(id);
            return new CalendarioNominaDto
            {
                Id = result.Id,
                QuincenaRestante = result.QuincenaRestante,
                NumeroQuincena = result.NumeroQuincena,
                FechaInicio = result.FechaInicio,
                FechaFinal = result.FechaFinal,
                Quincena = result.Quincena
            };
        }
        public async Task<List<CalendarioNominaDto>> GetQuincenasByPeriodoAsync(string fechaInicial, string fechaFinal)
        {
            var result = await _calendarioNominaRepository.GetQuincenasByPeriodoAsync(fechaInicial, fechaFinal);
            return result.Select(x => new CalendarioNominaDto
            {
                Id = x.Id,
                QuincenaRestante = x.QuincenaRestante,
                NumeroQuincena = x.NumeroQuincena,
                FechaInicio = x.FechaInicio,
                FechaFinal = x.FechaFinal,
                Quincena = x.Quincena
            }).ToList();
        }
    }
}
