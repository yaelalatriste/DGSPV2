using Catalogos.Persistence.Database;
using DGSP.Module.Catalogos.Application.Interfaces.Generales;
using DGSP.Module.Catalogos.Domain.Generales;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DGSP.Module.Catalogos.Persistence.Repositories.Generales
{
    public class CTMesRepository : ICTMesRepository
    {
        private readonly CatalogoDbContext _context;

        public CTMesRepository(CatalogoDbContext context)
        {
            _context = context;
        }

        public async Task<List<CTMes>> GetAllMesesAsync()
        {
            return await _context.CTMeses.AsNoTracking().ToListAsync();
        }

        public async Task<CTMes> GetMesByIdAsync(int id)
        {
            return await _context.CTMeses.Where(m => m.Id == id).AsNoTracking().FirstAsync();
        }
    }
}
