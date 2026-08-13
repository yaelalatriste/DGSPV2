using DGSP.Module.Seguros.Application.Interfaces.DGSP.Movimientos.Calculadora;
using DGSP.Module.Seguros.Domain.DGSP.Movimientos.Calculadora;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Seguros.Persistence.Repositories.DGSP.Movimientos.Calculadora
{
    public class CalendarioNominaRepository : ICalendarioNominaRepository
    {
        private readonly SegurosDbContext _context;

        public CalendarioNominaRepository(SegurosDbContext context)
        {
            _context = context;
        }

        public async Task<List<CalendarioNomina>> GetAllCalendarioAsync()
        {
            return await _context.CalendarioNominas.AsNoTracking().ToListAsync();
        }

        public async Task<List<CalendarioNomina>> GetQuincenasByPeriodoAsync(string fechaInicial, string fechaFinal)
        {
            DateTime inicio = DateTime.Parse(fechaInicial);
            DateTime fin = DateTime.Parse(fechaFinal);

            return await _context.CalendarioNominas
                .AsNoTracking()
                .Where(x => x.FechaInicio <= fin &&
                            x.FechaFinal >= inicio)
                .OrderBy(x => x.FechaInicio)
                .ToListAsync();
        }

        public async Task<CalendarioNomina> GetQuincenaById(int id)
        {
            return await _context.CalendarioNominas.AsNoTracking().Where(c => c.Id == id).FirstOrDefaultAsync() ?? new CalendarioNomina();
        }
    }
}
