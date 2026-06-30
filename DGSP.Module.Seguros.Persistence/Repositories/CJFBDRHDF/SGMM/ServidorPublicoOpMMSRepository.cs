using DGSP.Module.Seguros.Application.Interfaces.CJFBDRHDF.SGMM;
using DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.SGMM;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Seguros.Persistence.Repositories.CJFBDRHDF.SGMM
{
    public class ServidorPublicoOpMMSRepository : IServidorPublicoOpMMSRepository
    {
        private readonly SegurosSGMMContext _context;

        public ServidorPublicoOpMMSRepository(SegurosSGMMContext context)
        {
            _context = context;
        }

        public async Task<ServidorPublicoOpMMS> GetServidorPublicoOpMMS(int expediente)
        {
            var servidorPublico = await _context.ServidorPublicoOpMMS.AsNoTracking().Where(sp => sp.fiExpSP == expediente).FirstOrDefaultAsync();

            return servidorPublico ?? new ServidorPublicoOpMMS();
        }
    }
}
