using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Entradas;
using DGSP.Module.SMedicos.Domain.Inventarios;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.SMedicos.Persistence.Repositories.Medicamentos.Entradas
{
    public class LoteMedicamentoRepository : ILoteMedicamentoRepository
    {
        private readonly SMedicosDbContext _context;

        public LoteMedicamentoRepository(SMedicosDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LoteMedicamento entity)
        {
            await _context.LotesMedicamentos.AddAsync(entity);
        }

        public async Task<LoteMedicamento?> GetByIdAsync(int id)
        {
            return await _context.LotesMedicamentos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> ExistsAsync(int consultorioId, int medicamentoId, string lote, DateTime fechaCaducidad)
        {
            return await _context.LotesMedicamentos.AnyAsync(x =>
                x.ConsultorioId == consultorioId &&
                x.MedicamentoId == medicamentoId &&
                x.Lote == lote &&
                x.FechaCaducidad == fechaCaducidad);
        }
        
        public async Task<LoteMedicamento> GetLoteExistAsync(int consultorioId, int medicamentoId, string lote, DateTime fechaCaducidad)
        {
            var loteMedicamento = await _context.LotesMedicamentos.Where(x => x.ConsultorioId == consultorioId && x.MedicamentoId == medicamentoId &&
                                                               x.Lote == lote && x.FechaCaducidad == fechaCaducidad).FirstAsync();

            return loteMedicamento;
        }
        
        public async Task<LoteMedicamento> GetDatosByLoteAsync(string lote)
        {
            var loteMedicamento = await _context.LotesMedicamentos.Where(x => x.Lote == lote).FirstOrDefaultAsync();

            return loteMedicamento ?? new LoteMedicamento();
        }
        
        public async Task<LoteMedicamento> GetDatosByLoteConsultorioAsync(string lote, int consultorio)
        {
            var loteMedicamento = await _context.LotesMedicamentos.Where(x => x.Lote == lote && x.ConsultorioId == consultorio).FirstOrDefaultAsync();

            return loteMedicamento ?? new LoteMedicamento();
        }
        
        public async Task<LoteMedicamento> GetDatosByLoteConsultorioMedicamentoAsync(string lote, int consultorio, int medicamento)
        {
            var loteMedicamento = await _context.LotesMedicamentos.Where(x => x.Lote == lote && x.ConsultorioId == consultorio 
                                                                            && x.MedicamentoId == medicamento).FirstOrDefaultAsync();

            return loteMedicamento ?? new LoteMedicamento();
        }
        public async Task<List<LoteMedicamento>> GetMedicamentosByLoteConsultorioAsync(string lote, int consultorio)
        {
            var lotesMedicamento = await _context.LotesMedicamentos.Where(x => x.Lote == lote && x.ConsultorioId == consultorio).ToListAsync();

            return lotesMedicamento ?? new List<LoteMedicamento>();
        }

        public async Task<List<LoteMedicamento>> GetAllAsync()
        {
            return await _context.LotesMedicamentos.OrderBy(x => x.MedicamentoId).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
