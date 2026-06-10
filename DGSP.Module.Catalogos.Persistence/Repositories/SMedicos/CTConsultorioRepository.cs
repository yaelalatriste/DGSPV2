using Catalogos.Persistence.Database;
using DGSP.Module.Catalogos.Application.Interfaces.SMedicos;
using DGSP.Module.Catalogos.Domain.SMedicos;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Catalogos.Persistence.Repositories.SMedicos
{
    public class CTConsultorioRepository : ICTConsultorioRepository
    {
        private readonly CatalogoDbContext _context;

        public CTConsultorioRepository(CatalogoDbContext context)
        {
            _context = context;
        }       

        public async Task<List<CTConsultorio>> GetAllConsultoriosAsync()
        {
            return await _context.CTConsultorios.AsNoTracking().ToListAsync();
        }
        
        public async Task<CTConsultorio> GetConsulotorioByIdAsync(int id)
        {
            return await _context.CTConsultorios.AsNoTracking().Where(c => c.Id == id).FirstAsync();
        }

        public async Task RegistrarConsultorioAsync(CTConsultorio consultorio)
        {
            await _context.CTConsultorios.AddAsync(consultorio);
        }

        public async Task ActualizarConsultorioAsync(CTConsultorio consultorio)
        {
            _context.Update(consultorio);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
