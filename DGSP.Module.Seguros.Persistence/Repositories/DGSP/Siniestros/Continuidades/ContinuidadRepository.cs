using DGSP.Module.Seguros.Application.Interfaces.DGSP.Siniestros.Continuidades;
using DGSP.Module.Seguros.Domain.DGSP.Siniestros.Continuidades;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Seguros.Persistence.Repositories.DGSP.Siniestros.Continuidades
{
    public class ContinuidadRepository : IContinuidadRepository
    {
        private readonly SegurosDbContext _context;

        public ContinuidadRepository(SegurosDbContext context)
        {
            _context = context;
        }

        public async Task<List<Continuidad>> GetAllContinuidadesAsync()
        {
            var continuidades = await _context.Continuidades.AsNoTracking().Where(c => !c.FechaEliminacion.HasValue).ToListAsync();

            return continuidades;
        }
        public async Task<List<Continuidad>> GetContinuidadesPorPeriodoAsync(DateTime fechaInicio,DateTime fechaFin)
        {
            return await _context.Continuidades
                .AsNoTracking()
                .Where(c =>
                    !c.FechaEliminacion.HasValue &&
                    c.FechaCreacion.HasValue &&
                    c.FechaCreacion.Value >= fechaInicio &&
                    c.FechaCreacion.Value < fechaFin)
                .ToListAsync();
        }

        public async Task<Continuidad> GetContinuidadByIdAsync(int id)
        {
            var continuidades = await _context.Continuidades.AsNoTracking().Where(c => c.Id == id).FirstOrDefaultAsync() ?? new Continuidad();

            return continuidades;
        }

        public async Task<List<Continuidad>> GetContinuidadesByAnio(int anio)
        {
            var continuidades = await _context.Continuidades.AsNoTracking().Where(c => c.FechaBaja.GetValueOrDefault().Year == anio && !c.FechaEliminacion.HasValue).ToListAsync();

            return continuidades;
        }
       
        public async Task<List<Continuidad>> GetContinuidadesByEstatus(int estatus)
        {
            var continuidades = await _context.Continuidades.AsNoTracking().Where(c => c.EstatusId == estatus && !c.FechaEliminacion.HasValue).ToListAsync();

            return continuidades;
        }

        public async Task RegistrarContinuidadAsync(Continuidad command)
        {
            await _context.AddAsync(command);
        }

        public async Task ActualizarContinuidadAsync(Continuidad command)
        {
            _context.Update(command);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
