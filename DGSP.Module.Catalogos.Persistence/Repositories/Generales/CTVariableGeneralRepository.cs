using Catalogos.Persistence.Database;
using DGSP.Module.Catalogos.Application.Interfaces.Generales;
using DGSP.Module.Catalogos.Domain.Generales;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DGSP.Module.Catalogos.Persistence.Repositories.Generales
{
    public class CTVariableGeneralRepository : ICTVariableGeneralRepository
    {
        private readonly CatalogoDbContext _context;

        public CTVariableGeneralRepository(CatalogoDbContext context)
        {
            _context = context;
        }

        public async Task<List<CTVariableGeneral>> GetAllVariablesGeneralesAsync()
        {
            var variables = await _context.CTVariablesGenerales.AsNoTracking().ToListAsync();

            return variables;
        }

        public async Task<CTVariableGeneral> GetVariableGeneralById(int id)
        {
            var variable = await _context.CTVariablesGenerales.AsNoTracking().Where(v => v.Id == id).FirstOrDefaultAsync();

            return variable ?? new CTVariableGeneral();
        }

        public async Task<CTVariableGeneral> GetVariableGeneralxAnioAbreviacion(int anio, string abreviacion)
        {
            var variable = await _context.CTVariablesGenerales.AsNoTracking().Where(v => v.Anio == anio && v.Abreviacion.Equals(abreviacion)).FirstOrDefaultAsync();

            return variable ?? new CTVariableGeneral();
        }
    }
}
