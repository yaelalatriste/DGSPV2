using DGSP.Module.Seguros.Application.Interfaces.DGSP.Continuidades;
using DGSP.Module.Seguros.Domain.DGSP.Continuidades;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Seguros.Persistence.Repositories.DGSP.Continuidades
{
    public class OficioContinuidadRepository : IOficioContinuidadRepository
    {
        private readonly SegurosDbContext _context;

        public OficioContinuidadRepository(SegurosDbContext context)
        {
            _context = context;
        }

        public async Task<List<OficioContinuidad>> GetoficiosByContinuidadAsync(int continuidadId)
        {
            var oficios = await _context.OficiosContinuidades.Where(o => o.ContinuidadId == continuidadId).ToListAsync();

            return oficios;
        }

        public async Task RegistrarOficioContinuidadAsync(OficioContinuidad continuidad)
        {
            await _context.AddAsync(continuidad);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
