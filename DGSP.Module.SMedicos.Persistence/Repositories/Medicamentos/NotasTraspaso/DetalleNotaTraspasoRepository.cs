using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.NotasTraspaso;
using DGSP.Module.SMedicos.Domain.NotasTraspaso;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DGSP.Module.SMedicos.Persistence.Repositories.Medicamentos.NotasTraspaso
{
    public class DetalleNotaTraspasoRepository : IDetalleNotaTraspasoRepository
    {
        private readonly SMedicosDbContext _context;

        public DetalleNotaTraspasoRepository(SMedicosDbContext context)
        {
            _context = context;
        }

        public async Task<List<DetalleNotaTraspaso>> GetDetallesByNotaIdAsync(int nota)
        {
            return await _context.DetallesNotaTraspaso.AsNoTracking().Where(dt => dt.NotaId == nota && !dt.FechaEliminacion.HasValue).ToListAsync();
        }

        public async Task<DetalleNotaTraspaso> GetByIdAsync(int id)
        {
            return await _context.DetallesNotaTraspaso.AsNoTracking().Where(c => c.Id == id && !c.FechaEliminacion.HasValue).FirstOrDefaultAsync() ?? new DetalleNotaTraspaso();
        }

        public async Task AddDetalleNotaTraspasoAsync(DetalleNotaTraspaso command)
        {
            await _context.DetallesNotaTraspaso.AddAsync(command);
        }
        
        public void Remove(DetalleNotaTraspaso command)
        {
            _context.DetallesNotaTraspaso.Remove(command);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDetalleNotaTraspasoAsync(DetalleNotaTraspaso entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
