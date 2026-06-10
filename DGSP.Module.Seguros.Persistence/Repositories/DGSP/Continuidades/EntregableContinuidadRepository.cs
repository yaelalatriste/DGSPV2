using DGSP.Module.Seguros.Application.Interfaces.DGSP.Continuidades;
using DGSP.Module.Seguros.Domain.DGSP.Continuidades;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Seguros.Persistence.Repositories.DGSP.Continuidades
{
    public class EntregableContinuidadRepository : IEntregableContinuidadRepository
    {
        private readonly SegurosDbContext _context;

        public EntregableContinuidadRepository(SegurosDbContext context)
        {
            _context = context;
        }

        public async Task<List<EntregableContinuidad>> GetEntregablesByContinuidadAsync(int continuidadId)
        {
            var entregables = await _context.EntregablesContinuidades.AsNoTracking().Where(e => e.ContinuidadId == continuidadId && !e.FechaEliminacion.HasValue).ToListAsync();

            return entregables;
        }
        
        public async Task<EntregableContinuidad> GetEntregableByIdAsync(int id)
        {
            var entregable = await _context.EntregablesContinuidades.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync() ?? new EntregableContinuidad();

            return entregable;
        }

        public async Task RegistrarEntregableContinuidadAsync(EntregableContinuidad continuidad)
        {
            await _context.AddAsync(continuidad);
        }
        
        public async Task ActualizarEntregableContinuidadAsync(EntregableContinuidad continuidad)
        {
            _context.Update(continuidad);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
