using Catalogos.Persistence.Database;
using DGSP.Module.Catalogos.Application.Interfaces.SMedicos;
using DGSP.Module.Catalogos.Domain.SMedicos;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DGSP.Module.Catalogos.Persistence.Repositories.SMedicos
{
    public class CTMedicamentoRepository : ICTMedicamentoRepository
    {
        private readonly CatalogoDbContext _context;

        public CTMedicamentoRepository(CatalogoDbContext context)
        {
            _context = context;
        } 

        public async Task<List<CTMedicamento>> GetAllMedicamentosAsync()
        {
            return await _context.CTMedicamentos.AsNoTracking().ToListAsync();
        }

        public async Task<CTMedicamento> GetMedicamentoByIdAsync(int id)
        {
            return await _context.CTMedicamentos.AsNoTracking().Where(c => c.Id == id).FirstAsync();
        }

        public async Task RegistrarMedicamentoAsync(CTMedicamento medicamento)
        {
            await _context.CTMedicamentos.AddAsync(medicamento);
        }
        
        public async Task ActualizarMedicamentoAsync(CTMedicamento medicamento)
        {
            _context.Update(medicamento);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
