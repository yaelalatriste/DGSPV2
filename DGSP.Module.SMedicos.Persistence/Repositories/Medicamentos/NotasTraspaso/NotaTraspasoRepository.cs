using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.NotasTraspaso;
using DGSP.Module.SMedicos.Domain.NotasTraspaso;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.SMedicos.Persistence.Repositories.Medicamentos.NotasTraspaso
{
    public class NotaTraspasoRepository : INotaTraspasoRepository
    {
        private readonly SMedicosDbContext _context;

        public NotaTraspasoRepository(SMedicosDbContext context)
        {
            _context = context;
        }

        public async Task AddNotaTraspasoAsync(NotaTraspaso command)
        {
            await _context.NotasTraspaso.AddAsync(command);
        }
        
        public async Task ActualizarNotaTraspasoAsync(NotaTraspaso command)
        {
            _context.Update(command);
            await _context.SaveChangesAsync();
        }

        public async Task<List<NotaTraspaso>> GetAllNotasTraspaso()
        {
            return await _context.NotasTraspaso.AsNoTracking().ToListAsync();
        }

        public async Task<NotaTraspaso> GetNotaTraspasoById(int id)
        {
            return await _context.NotasTraspaso.AsNoTracking().Where(c => c.Id == id).FirstAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
