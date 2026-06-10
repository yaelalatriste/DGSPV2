using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Salidas;
using DGSP.Module.SMedicos.Domain.Inventarios;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.SMedicos.Persistence.Repositories.Medicamentos.Salidas
{
    public class SalidaMedicamentoDetalleRepository : ISalidaMedicamentoDetalleRepository
    {
        private readonly SMedicosDbContext _db;

        public SalidaMedicamentoDetalleRepository(SMedicosDbContext db)
        {
            _db = db;
        }

        public async Task<List<SalidaMedicamentoDetalle>> GetDetallesBySalidaIdAsync(int salidaId)
        {
            return await _db.SalidasMedicamentosDetalle.Where(x => x.SalidaId == salidaId).ToListAsync();
        }

        public async Task<SalidaMedicamentoDetalle> GetByIdAsync(int id)
        {
            return await _db.SalidasMedicamentosDetalle.FirstOrDefaultAsync(x => x.Id == id) ?? new SalidaMedicamentoDetalle();
        }

        public async Task AddAsync(SalidaMedicamentoDetalle entity)
        {
            await _db.SalidasMedicamentosDetalle.AddAsync(entity);
        }

        public void Remove(SalidaMedicamentoDetalle entity)
        {
            _db.SalidasMedicamentosDetalle.Remove(entity);
        }

        public Task SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
