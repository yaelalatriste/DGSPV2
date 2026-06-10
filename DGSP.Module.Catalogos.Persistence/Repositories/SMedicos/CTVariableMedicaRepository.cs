using Catalogos.Persistence.Database;
using DGSP.Module.Catalogos.Application.Interfaces.SMedicos;
using DGSP.Module.Catalogos.Domain.SMedicos;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Catalogos.Persistence.Repositories.SMedicos
{
    public class CTVariableMedicaRepository : ICTVariableMedicaRepository
    {
        private readonly CatalogoDbContext _context;

        public CTVariableMedicaRepository(CatalogoDbContext context)
        {
            _context = context;
        }

        public async Task<List<CTVariableMedica>> GetAllVariablesAsync()
        {
            return await _context.CTVariablesMedicas.AsNoTracking().ToListAsync();
        }
        
        public async Task<List<CTVariableMedica>> GetVariablesByCategoria(string categoria)
        {
            return await _context.CTVariablesMedicas.Where(v => v.Categoria.Contains(categoria)).ToListAsync();
        }

        public async Task<CTVariableMedica> GetVariableById(int id)
        {
            return await _context.CTVariablesMedicas.AsNoTracking().Where(c => c.Id == id).FirstOrDefaultAsync() ?? new CTVariableMedica();
        }
    }
}
