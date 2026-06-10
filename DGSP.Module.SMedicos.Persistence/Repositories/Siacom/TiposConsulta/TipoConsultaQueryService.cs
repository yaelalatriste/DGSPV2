using DGSP.Module.SMedicos.Application.Interfaces.Siacom.TipoConsulta;
using DGSP.Module.SMedicos.Persistence;
using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Catalogos;
using Microsoft.EntityFrameworkCore;
using SMedicos.Service.Queries.Mapping;

namespace SMedicos.Services.Queries.Queries.Siacom.TiposConsulta
{
    public class TipoConsultaQueryService : ITipoConsultaQueryService
    {
        private readonly SiacomDbContext _context;

        public TipoConsultaQueryService(SiacomDbContext context)
        {
            _context = context;
        }

        public async Task<List<CTTipoConsultaDto>> GetAllTiposConsultas()
        {
            var consultorios = await _context.CTTipoConsultas.ToListAsync();

            return consultorios.MapTo<List<CTTipoConsultaDto>>();
        }

        public async Task<CTTipoConsultaDto> GetTipoConsultaById(int id)
        {
            var consultorios = await _context.CTTipoConsultas.Where(c => c.FiIdTipoConsulta == id).FirstAsync();

            return consultorios.MapTo<CTTipoConsultaDto>();
        }
    }
}
