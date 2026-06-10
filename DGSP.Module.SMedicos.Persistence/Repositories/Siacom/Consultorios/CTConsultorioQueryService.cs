using DGSP.Module.SMedicos.Application.Interfaces.Siacom.Consultorios;
using DGSP.Module.SMedicos.Persistence;
using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Catalogos;
using Microsoft.EntityFrameworkCore;
using SMedicos.Service.Queries.Mapping;

namespace SMedicos.Services.Queries.Queries.Siacom.Consultorios
{
    public class CTConsultorioQueryService : ICTConsultorioQueryService
    {
        private readonly SiacomDbContext _context;

        public CTConsultorioQueryService(SiacomDbContext context)
        {
            _context = context;
        }

        public async Task<List<CTConsultorioSiacomDto>> GetAllConsultorios()
        {
            var consultorios = await _context.CTConsultorios.ToListAsync();

            return consultorios.MapTo<List<CTConsultorioSiacomDto>>();
        }

        public async Task<CTConsultorioSiacomDto> GetConsultorioById(int id)
        {
            var consultorios = await _context.CTConsultorios.Where(c => c.FiIdConsultorio == id).FirstAsync();

            return consultorios.MapTo<CTConsultorioSiacomDto>();
        }
    }
}
