using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Salidas;
using DGSP.Module.SMedicos.Domain.Inventarios;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.SMedicos.Persistence.Repositories.Medicamentos.Salidas
{
    public class SalidaMedicamentoRepository : ISalidaMedicamentoRepository
    {
        private readonly SMedicosDbContext _db;

        public SalidaMedicamentoRepository(SMedicosDbContext db)
        {
            _db = db;
        }

        public async Task<List<SalidaMedicamento>> GetAllSalidasMedicamentosAsync()
        {
            return await _db.SalidasMedicamentos.ToListAsync();
        }

        public async Task<SalidaMedicamento> GetByIdAsync(int id)
        {
            return await _db.SalidasMedicamentos.FirstOrDefaultAsync(x => x.Id == id) ?? new SalidaMedicamento();
        }

        public async Task AddAsync(SalidaMedicamento entity)
        {
            await _db.SalidasMedicamentos.AddAsync(entity);
        }

        public Task SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
